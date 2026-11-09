# 5. การใช้ AI ในโปรเจกต์ (AI Usage Disclosure)

> ⚠️ **ฉบับร่าง:** ให้สมาชิกกลุ่มอ่านทบทวนและแก้ให้ตรงกับที่ทำจริง ก่อนส่งอาจารย์
> ตามข้อกำหนดของวิชา: *"ให้แจ้งด้วยว่าใช้ AI ที่ส่วนใดบ้าง และผลเป็นอย่างไร"*

## AI ที่ใช้

- **Claude (Anthropic)** ผ่าน Claude Code (แท็บ Code ใน Claude desktop app)

## สิ่งที่กลุ่มเป็นคนตัดสินใจ (คุม Design)

| เรื่อง | การตัดสินใจของกลุ่ม |
|---|---|
| Presentation Layer | เลือก **WinForms (C#)** ให้ตรงกับ Lab ชีต 12 |
| ขอบเขตฟีเจอร์ | ค้นหาเกม**ตามชื่อและ genre เท่านั้น**, ตะกร้าสินค้า, ขอคืนเงิน และ**ไม่ทำ**ส่วนเพิ่มเกมของผู้ขาย |
| Database | สร้าง role/database เองใน pgAdmin4 ตามขั้นตอนของวิชา |
| *(ให้กลุ่มเติม)* | *เช่น กฎคืนเงิน 14 วัน / 2 ชั่วโมง, ชื่อ use case, การแบ่งงานในกลุ่ม ...* |

## ส่วนที่ใช้ AI ช่วย และผลลัพธ์

| ส่วน | AI ช่วยทำอะไร | ผลเป็นอย่างไร / สิ่งที่ตรวจสอบ |
|---|---|---|
| อ่านสไลด์ | สรุปหลักการจากชีต 10-12 เพื่อใช้ออกแบบ | ใช้เป็น checklist ว่าต้องมี C4, use case, ER, REST, EF scaffold |
| Database | ร่าง SQL script (table, FK, test data) และไฟล์ DBML | รันผ่านใน PostgreSQL 18, ข้อมูลภาษาไทยแสดงถูกต้อง |
| Data Access Layer | รันคำสั่ง `dotnet ef dbcontext scaffold` และเขียน `Model.cs`, `GameStoreContext.Custom.cs`, `SimulatedBankGateway` | scaffold ได้ 9 model ตรงกับตาราง |
| Domain Layer + REST API | เขียน Services, Controllers, `RefundPolicy`, `PasswordHasher` และ error handler | ทดสอบทุก end point ด้วย HTTP request ทั้งกรณีสำเร็จและกรณี error (400/401/402/404/409) |
| Presentation Layer | เขียน WinForms ทุกหน้าจอ, `RestUtil`, `StoreApi` | build ผ่าน, ถ่ายภาพทุกหน้าจอตรวจ layout แล้ว และทดสอบทุกการเรียก API ของ client แล้ว **แต่ยังไม่ได้เปิดใน Form Designer ของ Visual Studio** (ให้กลุ่มตรวจ) |
| Tests | เขียน unit test 13 ตัว (กฎคืนเงิน, password hash) | ผ่านทั้งหมด |
| Diagrams / เอกสาร | ร่าง use case, C4 (Structurizr DSL), PlantUML, ER, เอกสารนี้ | ตรวจกับรูปแบบในชีต 10 (System / Primary Actor / Goal Level, legend, เส้นมีคำอธิบาย) |

## ข้อผิดพลาดของ AI ที่พบระหว่างทำ (AI ก็ผิดได้) และการแก้ไข

ข้อผิดพลาดเหล่านี้เกิดขึ้นจริงระหว่างพัฒนา และถูกจับได้จากการทดสอบ:

1. **หน้าจอ WinForms เล็กผิดปกติและปุ่มถูกตัด**: ค่า `AutoScaleDimensions` ที่ AI ใส่ไว้ (9, 23) ไม่ตรงกับฟอนต์ Leelawadee UI จริง หลังวัดค่าได้ (7, 17) จึงแก้ทุกฟอร์ม
2. **ลายบนปกเกมวาดล้นออกนอกกรอบ** ไปทับข้อความ แก้ด้วยการ clip พื้นที่วาด
3. **เวอร์ชัน Entity Framework ไม่ตรงกัน** (10.0.4 กับ 10.0.14) มี warning ตอน build แก้โดย pin เวอร์ชันให้ตรงกัน
4. **วันที่คืนเงินใน response มี timezone `+07:00`** ไม่ตรงกับ endpoint อื่น แก้ให้อ่านค่าจาก database ซ้ำ
5. **กดซื้อพร้อมกันหลายครั้งอาจได้ error 500**: เพิ่มการแปลง `DbUpdateConcurrencyException` เป็น 409 แล้วทดสอบยิงพร้อมกัน 3 request ผลคือสำเร็จ 1 ครั้ง และตัดเงินครั้งเดียว
6. **URL `users:login`** ถ้าใช้ `new Uri(base, "users:login")` จะถูกตีความผิดเป็น scheme ชื่อ `users` จึงเปลี่ยนเป็นต่อ string เอง (มี comment อธิบายไว้ใน `RestUtil.cs`)

## สิ่งที่สมาชิกต้องเข้าใจเอง (เผื่อต้องแก้ไข)

- [ ] อธิบายได้ว่า request จาก Client เดินผ่าน layer ไหนบ้าง จนถึง database (ดู [02-architecture.md](02-architecture.md))
- [ ] อธิบายได้ว่าทำไมการตัดเงินกับสร้าง order ต้องอยู่ใน transaction เดียวกัน
- [ ] scaffold EF ใหม่ได้เมื่อแก้ตาราง (ดู [03-database.md](03-database.md))
- [ ] เพิ่ม end point ใหม่ได้: เพิ่ม method ใน Service → เพิ่ม action ใน Controller → เพิ่ม method ใน `StoreApi` → เรียกจาก Form
- [ ] เปิดทุกฟอร์มใน Form Designer ของ Visual Studio ได้ (AI ยังไม่ได้ตรวจส่วนนี้)
- [ ] วาง `docs/c4/workspace.dsl` ใน playground.structurizr.com และวาง `.puml` ใน PlantUML แล้ว generate ภาพได้ (AI ยังไม่ได้ parse ไฟล์ต้นฉบับเหล่านี้ด้วยเครื่องมือจริง ภาพใน `docs/diagrams` วาดแยกไว้)
- [ ] *(ให้กลุ่มเติม)*
