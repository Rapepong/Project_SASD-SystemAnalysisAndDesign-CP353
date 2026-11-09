/*
 * C4 Model ของ Online Game Store (OGS) — ภาษา Structurizr DSL
 * วางโค้ดทั้งไฟล์นี้ที่ https://playground.structurizr.com/ เพื่อ generate ภาพ
 *   - System Context diagram
 *   - Container diagram
 *   - Component diagram (ของ Game Store API)
 * (Code diagram ดูที่ docs/uml/code-diagram.puml)
 *
 * หลักการตามชีต 10: กล่องมี ชื่อ + ประเภท + คำอธิบาย, เส้นมีคำอธิบายที่อ่านเป็นประโยคตามทิศลูกศร,
 * เส้นเป็นเส้นประทิศทางเดียว และมี Legend อธิบายสี/รูปร่าง
 */
workspace "Online Game Store" "CP353 System Analysis and Design - โจทย์ B" {

    model {
        customer = person "Customer (ลูกค้า)" "ผู้ที่เลือกซื้อเกม เล่นเกมจาก Game List และขอคืนเงินได้"

        bank = softwareSystem "Bank System (ธนาคาร)" "ระบบธนาคาร ตัดเงินและคืนเงิน (จำลอง ไม่ได้เชื่อมต่อธนาคารจริง)" "External"

        ogs = softwareSystem "Online Game Store (OGS)" "ร้านขายเกมออนไลน์: ค้นหาเกม ตะกร้า ชำระเงิน Game List และคืนเงิน" {

            client = container "Game Store Client" "หน้าจอให้ลูกค้าใช้งาน (Presentation Layer) เรียก REST API ผ่าน HttpClient" "C# WinForms, .NET 10" "Desktop"

            api = container "Game Store API" "Business Logic ทั้งหมด (Domain Layer) + Data Access Layer เปิดให้เรียกผ่าน REST API" "ASP.NET Core 10 Web API" {
                group "REST API (Controllers)" {
                    usersController = component "UsersController" "POST /users, POST /users:login" "ASP.NET Controller"
                    catalogController = component "CatalogController" "GET /games?search=&genreId=, GET /genres" "ASP.NET Controller"
                    cartController = component "CartController" "GET/POST/DELETE /users/{id}/cart/items" "ASP.NET Controller"
                    ordersController = component "OrdersController" "POST/GET /users/{id}/orders" "ASP.NET Controller"
                    libraryController = component "LibraryController" "GET /users/{id}/library, POST .../{gameId}:play" "ASP.NET Controller"
                    refundsController = component "RefundsController" "POST/GET /users/{id}/refunds" "ASP.NET Controller"
                }
                group "Domain Layer (Business Logic)" {
                    accountService = component "AccountService" "สมัครสมาชิก ตรวจ username ซ้ำ ตรวจรหัสผ่าน" "C# class"
                    catalogService = component "CatalogService" "ค้นหาเกมตามชื่อและ genre" "C# class"
                    cartService = component "CartService" "เพิ่ม/ลบเกมในตะกร้า ห้ามซ้ำ ห้ามซื้อเกมที่มีแล้ว" "C# class"
                    purchaseService = component "PurchaseService" "ชำระเงินใน transaction เดียว: ตัดเงิน สร้าง order เพิ่ม Game List" "C# class"
                    libraryService = component "LibraryService" "Game List และจำลองการเล่นเกม" "C# class"
                    refundService = component "RefundService" "คืนเงินตามเงื่อนไข และนำเกมออกจาก Game List" "C# class"
                    refundPolicy = component "RefundPolicy" "กฎ: ภายใน 14 วัน และเล่นไม่ถึง 2 ชั่วโมง" "C# static class"
                    passwordHasher = component "PasswordHasher" "hash รหัสผ่านด้วย PBKDF2 + salt" "C# static class"
                }
                group "Data Access Layer" {
                    dbContext = component "GameStoreContext" "Data Mapper (ORM): แปลง object <-> table, LINQ -> SQL" "EF Core DbContext"
                    bankGateway = component "SimulatedBankGateway" "Gateway ไปยังธนาคาร (implements IBankGateway)" "C# class"
                }
            }

            database = container "Database" "เก็บข้อมูล users, games, genres, cart, orders, Game List, refunds" "PostgreSQL 18" "Database"
        }

        # ---------- ความสัมพันธ์ (อ่านเป็นประโยคตามทิศลูกศร) ----------
        customer -> client "ค้นหา ซื้อ เล่น และขอคืนเงินเกม โดยใช้"

        client -> usersController "สมัคร/login โดยเรียก" "JSON/HTTP"
        client -> catalogController "ค้นหาเกมโดยเรียก" "JSON/HTTP"
        client -> cartController "จัดการตะกร้าโดยเรียก" "JSON/HTTP"
        client -> ordersController "ชำระเงินโดยเรียก" "JSON/HTTP"
        client -> libraryController "ดู Game List โดยเรียก" "JSON/HTTP"
        client -> refundsController "ขอคืนเงินโดยเรียก" "JSON/HTTP"

        usersController -> accountService "ส่งงานต่อให้"
        catalogController -> catalogService "ส่งงานต่อให้"
        cartController -> cartService "ส่งงานต่อให้"
        ordersController -> purchaseService "ส่งงานต่อให้"
        libraryController -> libraryService "ส่งงานต่อให้"
        refundsController -> refundService "ส่งงานต่อให้"

        accountService -> passwordHasher "hash/ตรวจรหัสผ่านด้วย"
        libraryService -> refundPolicy "คำนวณสิทธิ์คืนเงินด้วย"
        refundService -> refundPolicy "ตรวจเงื่อนไขคืนเงินด้วย"
        purchaseService -> bankGateway "ขอตัดเงินผ่าน"
        refundService -> bankGateway "ขอคืนเงินผ่าน"

        accountService -> dbContext "อ่าน/เขียน users ผ่าน" "LINQ"
        catalogService -> dbContext "query games, genres ผ่าน" "LINQ"
        cartService -> dbContext "อ่าน/เขียน cart_items ผ่าน" "LINQ"
        purchaseService -> dbContext "บันทึก orders, library_items ผ่าน" "LINQ + Transaction"
        libraryService -> dbContext "อ่าน/เขียน library_items ผ่าน" "LINQ"
        refundService -> dbContext "บันทึก refunds ผ่าน" "LINQ + Transaction"

        dbContext -> database "อ่านและเขียนข้อมูลลง" "SQL (Npgsql, TCP 5432)"
        bankGateway -> bank "ส่งคำขอตัดเงิน/คืนเงินไปยัง" "จำลองด้วย table bank_accounts"
    }

    views {
        systemContext ogs "SystemContext" "[System Context] Online Game Store" {
            include *
            autoLayout lr
        }

        container ogs "Containers" "[Container] Online Game Store - 3 Tier" {
            include *
            autoLayout lr
        }

        component api "Components" "[Component] Game Store API" {
            include *
            autoLayout tb
        }

        styles {
            element "Element" {
                fontSize 22
            }
            element "Person" {
                shape Person
                background #08427b
                color #ffffff
            }
            element "Software System" {
                background #1168bd
                color #ffffff
            }
            element "External" {
                background #999999
                color #ffffff
            }
            element "Container" {
                background #438dd5
                color #ffffff
            }
            element "Desktop" {
                shape Window
            }
            element "Database" {
                shape Cylinder
            }
            element "Component" {
                background #85bbf0
                color #000000
            }
            relationship "Relationship" {
                dashed true
                color #707070
            }
        }
    }
}
