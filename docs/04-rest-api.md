# 4. REST API Design (Domain Layer)

Base URL: `http://localhost:5080/api/v1`

## 4.1 หลักการออกแบบ (ชีต 12 หน้า 19)

| หลักการ | ทำอย่างไรในโปรเจกต์นี้ |
|---|---|
| 1. ใช้ HTTP methods เป็น action | `GET` = อ่าน, `POST` = สร้าง, `DELETE` = ลบ |
| 2. ใช้คำนามเป็น resource | `/games`, `/users/{id}/cart`, `/users/{id}/orders` ใช้พหูพจน์และตัวพิมพ์เล็ก ส่วนของที่เป็นของ user จะอยู่เป็น sub-collection ใต้ `/users/{id}` |
| 3. ใช้ status code บอกผล | ดูตาราง 4.3 |
| 4. ใช้ query parameter กรองข้อมูล | `GET /games?search=star&genreId=1` |
| 5. ใช้ JSON | ทั้ง request และ response (camelCase) |
| 6. มี versioning | ทุก URL ขึ้นต้นด้วย `/api/v1/` |
| Custom method (ชีต 12 หน้า 24) | ใช้รูปแบบเดียวกับ Google: `POST /users:login`, `POST /users/{id}/library/{gameId}:play` |

การซื้อเกมใช้ `POST /users/{id}/orders` (การจ่ายเงินคือ "สร้างคำสั่งซื้อใหม่") และการคืนเงินใช้ `POST /users/{id}/refunds` ("สร้างรายการคืนเงินใหม่") ซึ่งเป็นคำนามตามหลัก REST จึงไม่ต้องตั้งชื่อ URL เป็นคำกริยาอย่าง `/buyGame`

## 4.2 End Points ทั้งหมด

| Method | URL | ทำอะไร | Success | Error ที่เป็นไปได้ |
|---|---|---|---|---|
| POST | `/users` | สมัครสมาชิก | 201 Created | 400, 409 (username ซ้ำ) |
| POST | `/users:login` | เข้าสู่ระบบ | 200 OK | 401 |
| GET | `/users/{userId}` | ข้อมูลผู้ใช้ | 200 | 404 |
| GET | `/genres` | แนวเกมทั้งหมด | 200 | |
| GET | `/games?search=&genreId=` | ค้นหาเกม (ชื่อ / genre) | 200 | |
| GET | `/games/{gameId}` | รายละเอียดเกม | 200 | 404 |
| GET | `/users/{userId}/cart` | ดูตะกร้า | 200 | 404 |
| POST | `/users/{userId}/cart/items` | เพิ่มเกมลงตะกร้า `{ "gameId": 3 }` | 201 | 404, 409 (มีแล้ว/อยู่ในตะกร้า/ไม่วางขาย) |
| DELETE | `/users/{userId}/cart/items/{gameId}` | นำเกมออกจากตะกร้า | 204 No Content | 404 |
| POST | `/users/{userId}/orders` | **ชำระเงิน** `{ "bankAccountNo": "111-1-11111-1" }` | 201 | 400 (ตะกร้าว่าง), **402** (ธนาคารปฏิเสธ), 409 |
| GET | `/users/{userId}/orders` | ประวัติการซื้อ | 200 | 404 |
| GET | `/users/{userId}/orders/{orderId}` | คำสั่งซื้อ 1 รายการ | 200 | 404 |
| GET | `/users/{userId}/library` | **Game List** | 200 | 404 |
| POST | `/users/{userId}/library/{gameId}:play` | เล่นเกม (จำลอง +30 นาที) | 200 | 404 |
| POST | `/users/{userId}/refunds` | **ขอคืนเงิน** `{ "orderItemId": 4, "reason": "..." }` | 201 | 404, 409 (ผิดเงื่อนไข), 502 |
| GET | `/users/{userId}/refunds` | ประวัติการคืนเงิน | 200 | 404 |
| GET | `/users/{userId}/refunds/{refundId}` | การคืนเงิน 1 รายการ | 200 | 404 |

## 4.3 Status Codes ที่ใช้

| Code | ความหมายในระบบนี้ | มาจาก |
|---|---|---|
| 200 OK | สำเร็จ | |
| 201 Created | สร้าง resource ใหม่สำเร็จ (มี header `Location` ชี้ไป resource ใหม่) | `CreatedAtAction(...)` |
| 204 No Content | ลบสำเร็จ | `NoContent()` |
| 400 Bad Request | ข้อมูลที่ส่งมาไม่ถูกต้อง เช่น username สั้นไป หรือตะกร้าว่าง | `DomainException.Validation` |
| 401 Unauthorized | login ไม่ผ่าน | `DomainException.Unauthorized` |
| 402 Payment Required | ธนาคารปฏิเสธการตัดเงิน (เงินไม่พอ / ไม่พบบัญชี) | `DomainException.PaymentDeclined` |
| 404 Not Found | ไม่พบ user / เกม / รายการ | `DomainException.NotFound` |
| 409 Conflict | ขัดกับสถานะปัจจุบัน เช่น มีเกมแล้ว หรือเลยกำหนดคืนเงิน | `DomainException.Conflict`, unique constraint |
| 500 Internal Server Error | ข้อผิดพลาดที่ไม่คาดคิด | อื่น ๆ |
| 502 Bad Gateway | ระบบธนาคาร (ภายนอก) ทำงานไม่สำเร็จ | `DomainException.ExternalService` |

## 4.4 รูปแบบ Error (Problem Details)

ทุก error ส่ง body แบบมาตรฐาน RFC 9457 ฝั่ง client จึงอ่านข้อความ `detail` ไปแสดงได้เลย

```http
HTTP/1.1 402 Payment Required
Content-Type: application/problem+json

{
  "title": "Payment Required",
  "status": 402,
  "detail": "ธนาคารปฏิเสธการชำระเงิน: ยอดเงินในบัญชีไม่เพียงพอ"
}
```

## 4.5 วิธีทดสอบ API

1. Run `GameStore.Api` (profile `http`) แล้ว browser จะเปิด `http://localhost:5080/openapi/v1.json` (OpenAPI spec ที่ ASP.NET สร้างให้อัตโนมัติ)
2. เปิดไฟล์ [`GameStore.Api/GameStore.Api.http`](../GameStore.Api/GameStore.Api.http) ใน Visual Studio แล้วกด **Send request** เหนือแต่ละ request (หรือใช้ View > Other Windows > Endpoints Explorer)
3. ทดสอบ GET ผ่าน web browser ได้ เช่น <http://localhost:5080/api/v1/games?search=star>

## 4.6 ข้อจำกัดที่รู้อยู่แล้ว

- ยังไม่มี authentication token: หลัง login ตัว client ส่ง `userId` ไปใน URL ถ้าเป็นระบบจริงควรใช้ JWT หรือ session token และตรวจที่ server ว่า token ตรงกับ `userId` (ไม่ได้ทำเพราะอยู่นอกเนื้อหาวิชา)
- ใช้ HTTP ไม่ใช่ HTTPS เพื่อลดความยุ่งยากเรื่อง certificate ตามคำแนะนำท้ายชีต 12
