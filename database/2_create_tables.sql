-- =====================================================================
--  Online Game Store : 2) สร้าง TABLES + ใส่ข้อมูลทดสอบ (Test Data)
-- ---------------------------------------------------------------------
--  รันด้วย pgAdmin4: เปิด Query Tool ของ database "gamestore" แล้วรันทั้งไฟล์ (F5)
--  รันซ้ำได้ ถ้าต้องการ reset ข้อมูลกลับเป็นค่าเริ่มต้น (จะ DROP TABLE ทิ้งก่อน)
--
--  หลักการออกแบบ (จากชีตที่ 11 Data Access Layer)
--  * ใช้ Surrogate Key (INTEGER GENERATED ALWAYS AS IDENTITY) เป็น primary key
--    ยกเว้น genres ที่เป็นตาราง lookup ขนาดเล็ก กำหนด id เอง (เหมือนตาราง items ในตัวอย่าง)
--  * ชื่อ table/column เป็น snake_case เพื่อให้ EF แปลงเป็น PascalCase ได้
--  * เงินใช้ type money (เก็บทศนิยมฐานสิบ ไม่ใช่ floating point)
--  * ทุก column เป็น atomic, ความสัมพันธ์ many-to-many (games <-> genres) ใช้ table กลาง game_genres
-- =====================================================================

-- ให้ table ที่สร้างเป็นของ gamestore_app (กรณี login ด้วย postgres)
SET ROLE gamestore_app;

------------------DROP TABLES (สำหรับ reset)---------------------
DROP TABLE IF EXISTS refunds, library_items, order_items, orders,
                     cart_items, game_genres, games, genres, users,
                     bank_accounts CASCADE;

------------------CREATE TABLES---------------------------------
CREATE TABLE "users" (
  "id" INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  "username" varchar(50) NOT NULL UNIQUE,
  "password_hash" varchar(200) NOT NULL,
  "display_name" varchar(100) NOT NULL,
  "email" varchar(100) NOT NULL,
  "created_at" timestamp NOT NULL DEFAULT LOCALTIMESTAMP
);

CREATE TABLE "genres" (
  "id" INTEGER PRIMARY KEY,
  "name" varchar(50) NOT NULL UNIQUE
);

CREATE TABLE "games" (
  "id" INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  "title" varchar(150) NOT NULL,
  "developer" varchar(100) NOT NULL,
  "description" varchar(2000) NOT NULL DEFAULT '',
  "price" money NOT NULL,
  "release_date" date NOT NULL,
  "is_active" boolean NOT NULL DEFAULT true
);

-- table กลางของความสัมพันธ์ many-to-many ระหว่าง games กับ genres
CREATE TABLE "game_genres" (
  "game_id" integer NOT NULL,
  "genre_id" integer NOT NULL,
  PRIMARY KEY ("game_id", "genre_id")
);

CREATE TABLE "cart_items" (
  "id" INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  "user_id" integer NOT NULL,
  "game_id" integer NOT NULL,
  "added_at" timestamp NOT NULL DEFAULT LOCALTIMESTAMP,
  UNIQUE ("user_id", "game_id")
);

CREATE TABLE "orders" (
  "id" INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  "user_id" integer NOT NULL,
  "ordered_at" timestamp NOT NULL,
  "total_amount" money NOT NULL,
  "bank_account_no" varchar(20) NOT NULL,
  "payment_ref" varchar(40) NOT NULL
);

-- price = ราคา ณ วันที่ซื้อ (ถ้าภายหลังร้านเปลี่ยนราคาเกม ประวัติการซื้อจะไม่เปลี่ยนตาม)
CREATE TABLE "order_items" (
  "id" INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  "order_id" integer NOT NULL,
  "game_id" integer NOT NULL,
  "price" money NOT NULL,
  "is_refunded" boolean NOT NULL DEFAULT false
);

-- Game List (คลังเกมของลูกค้า) : 1 user มีเกมเดียวกันได้แค่ 1 ชิ้น
CREATE TABLE "library_items" (
  "id" INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  "user_id" integer NOT NULL,
  "game_id" integer NOT NULL,
  "order_item_id" integer NOT NULL UNIQUE,
  "acquired_at" timestamp NOT NULL,
  "playtime_minutes" integer NOT NULL DEFAULT 0,
  "last_played_at" timestamp,
  UNIQUE ("user_id", "game_id")
);

CREATE TABLE "refunds" (
  "id" INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  "order_item_id" integer NOT NULL UNIQUE,
  "amount" money NOT NULL,
  "reason" varchar(500) NOT NULL DEFAULT '',
  "refunded_at" timestamp NOT NULL,
  "payment_ref" varchar(40) NOT NULL
);

-- ธนาคารจำลอง (Simulated Bank) : แทนระบบธนาคารภายนอก ไม่ได้เชื่อมต่อธนาคารจริง
-- โค้ดฝั่ง Domain จะเข้าถึง table นี้ผ่าน IBankGateway เท่านั้น
CREATE TABLE "bank_accounts" (
  "id" INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  "account_no" varchar(20) NOT NULL UNIQUE,
  "owner_name" varchar(100) NOT NULL,
  "balance" money NOT NULL
);

------------------FOREIGN KEYS----------------------------------
ALTER TABLE "game_genres" ADD FOREIGN KEY ("game_id") REFERENCES "games" ("id") DEFERRABLE INITIALLY IMMEDIATE;
ALTER TABLE "game_genres" ADD FOREIGN KEY ("genre_id") REFERENCES "genres" ("id") DEFERRABLE INITIALLY IMMEDIATE;

ALTER TABLE "cart_items" ADD FOREIGN KEY ("user_id") REFERENCES "users" ("id") DEFERRABLE INITIALLY IMMEDIATE;
ALTER TABLE "cart_items" ADD FOREIGN KEY ("game_id") REFERENCES "games" ("id") DEFERRABLE INITIALLY IMMEDIATE;

ALTER TABLE "orders" ADD FOREIGN KEY ("user_id") REFERENCES "users" ("id") DEFERRABLE INITIALLY IMMEDIATE;

ALTER TABLE "order_items" ADD FOREIGN KEY ("order_id") REFERENCES "orders" ("id") DEFERRABLE INITIALLY IMMEDIATE;
ALTER TABLE "order_items" ADD FOREIGN KEY ("game_id") REFERENCES "games" ("id") DEFERRABLE INITIALLY IMMEDIATE;

ALTER TABLE "library_items" ADD FOREIGN KEY ("user_id") REFERENCES "users" ("id") DEFERRABLE INITIALLY IMMEDIATE;
ALTER TABLE "library_items" ADD FOREIGN KEY ("game_id") REFERENCES "games" ("id") DEFERRABLE INITIALLY IMMEDIATE;
ALTER TABLE "library_items" ADD FOREIGN KEY ("order_item_id") REFERENCES "order_items" ("id") DEFERRABLE INITIALLY IMMEDIATE;

ALTER TABLE "refunds" ADD FOREIGN KEY ("order_item_id") REFERENCES "order_items" ("id") DEFERRABLE INITIALLY IMMEDIATE;

------------------INSERT Base Tables----------------------------
-- รหัสผ่านของทั้งสอง user คือ pass1234 (เก็บเป็น PBKDF2 hash ไม่เก็บรหัสผ่านจริง)
INSERT INTO users (username, password_hash, display_name, email) VALUES
('player1', 'PBKDF2-SHA256$100000$oSGv+WQMD9xepHHPk93Fxw==$HqGyY21Ea7RJAedvNm55z1HNYnhvV9egZN3Ts9So3Go=', 'Somchai Jaidee', 'somchai@example.com'),
('player2', 'PBKDF2-SHA256$100000$Uz3cFKXS/i9m0j0Ro2bSLw==$1dPPddkl9Kc8gCkreZWR3h7zrrnoeZHkDFs7PDiP+A0=', 'Suda Rakgame', 'suda@example.com');

INSERT INTO genres VALUES
(1, 'Action'),
(2, 'Adventure'),
(3, 'RPG'),
(4, 'Strategy'),
(5, 'Simulation'),
(6, 'Sports'),
(7, 'Racing'),
(8, 'Puzzle'),
(9, 'Horror'),
(10, 'Indie');

INSERT INTO games (title, developer, description, price, release_date, is_active) VALUES
('Starfall Odyssey',  'Nebula Forge Studio', 'ผจญภัยข้ามกาแล็กซีในโลกเปิดขนาดใหญ่ สร้างยานและทีมของคุณเอง', 1290, '2025-03-14', true),  -- 1
('Kingdoms of Ash',   'Ironclad Games',      'วางแผนสร้างอาณาจักรจากเถ้าถ่าน บริหารทรัพยากรและทำสงครามแบบ turn-based', 990, '2024-10-02', true), -- 2
('Neon Rush',         'Pixel Pulse',         'แข่งรถความเร็วสูงในเมืองแสงนีออน พร้อมดนตรี synthwave', 459, '2025-06-20', true),                 -- 3
('Farmstead Days',    'Cozy Corner',         'ปลูกผัก เลี้ยงสัตว์ และใช้ชีวิตชิล ๆ ในหมู่บ้านชนบท', 349, '2023-11-11', true),                   -- 4
('The Silent Ward',   'Dark Lantern',        'สยองขวัญเอาชีวิตรอดในโรงพยาบาลร้างกลางดึก', 599, '2024-10-31', true),                            -- 5
('Block Logic',       'Tiny Owl',            'เกมปริศนาต่อบล็อกกว่า 300 ด่าน ฝึกสมองแบบสบาย ๆ', 129, '2022-05-05', true),                     -- 6
('Goal Masters 26',   'Arena Sports',        'เกมฟุตบอลที่สมจริงที่สุดของปี พร้อมโหมดออนไลน์', 1590, '2025-09-26', true),                     -- 7
('Sky Pirates',       'Cloudbreak',          'โจรสลัดเหาะเหินบนเกาะลอยฟ้า ต่อสู้ด้วยเรือเหาะ', 699, '2024-04-18', true),                      -- 8
('Dungeon Delvers',   'Rune Crafters',       'ตะลุยดันเจี้ยนแบบ roguelike สุ่มแผนที่ใหม่ทุกครั้ง', 399, '2023-08-08', true),                    -- 9
('Orbital Tycoon',    'Gravity Well',        'สร้างและบริหารสถานีอวกาศให้มีกำไร', 759, '2025-01-22', true),                                     -- 10
('Shadow Blade',      'Crimson Moon',        'แอ็กชันดาบนินจาความเร็วสูง คอมโบไม่รู้จบ', 890, '2025-07-07', true),                            -- 11
('Mystic Gardens',    'Lotus Leaf',          'ไขปริศนาในสวนเวทมนตร์ที่เปลี่ยนไปตามฤดูกาล', 199, '2023-03-03', true),                          -- 12
('Drift Kings',       'Turbo Labs',          'ดริฟต์บนภูเขาและสนามแข่ง แต่งรถได้ละเอียด', 529, '2024-12-12', true),                            -- 13
('Echoes Below',      'Hollow Point',        'สำรวจถ้ำมืดที่มีเสียงสะท้อนเป็นเบาะแสเดียว', 459, '2025-02-28', true),                           -- 14
('Legends of Siam',   'Golden Naga',         'RPG ย้อนยุคอยุธยา ต่อสู้กับอสูรในตำนานไทย', 1090, '2025-10-15', true),                           -- 15
('Tactics Arena',     'Ironclad Games',      'วางแผนจัดทีมฮีโร่ประลองบนกระดานแบบ PvP', 299, '2024-06-01', true),                               -- 16
('Retro Racer',       'Old School Inc.',     'เกมแข่งรถยุค 90 (ถอดออกจากร้านแล้ว)', 99, '2010-01-01', false);                                  -- 17

INSERT INTO game_genres (game_id, genre_id) VALUES
(1, 1), (1, 2), (1, 3),
(2, 4),
(3, 7), (3, 1),
(4, 5), (4, 10),
(5, 9), (5, 2),
(6, 8), (6, 10),
(7, 6),
(8, 1), (8, 2),
(9, 3), (9, 10),
(10, 5), (10, 4),
(11, 1),
(12, 8), (12, 2),
(13, 7), (13, 6),
(14, 9), (14, 10),
(15, 3), (15, 1), (15, 2),
(16, 4),
(17, 7);

-- ธนาคารจำลอง: บัญชีสำหรับทดสอบ
INSERT INTO bank_accounts (account_no, owner_name, balance) VALUES
('111-1-11111-1', 'Somchai Jaidee', 5000),     -- เงินพอซื้อได้หลายเกม
('222-2-22222-2', 'Suda Rakgame', 300),        -- เงินน้อย ใช้ทดสอบกรณีเงินไม่พอ
('999-9-99999-9', 'Demo Rich Account', 100000);

------------------INSERT Test Order Data------------------------
-- player1 (user id 1) ซื้อเกมไปแล้ว 3 orders เพื่อใช้ทดสอบกฎการคืนเงิน
INSERT INTO orders (user_id, ordered_at, total_amount, bank_account_no, payment_ref) VALUES
(1, LOCALTIMESTAMP - INTERVAL '20 days', 528, '111-1-11111-1', 'PAY-SEED-0001'),  -- 1 : เกิน 14 วัน -> คืนเงินไม่ได้
(1, LOCALTIMESTAMP - INTERVAL '3 days',  990, '111-1-11111-1', 'PAY-SEED-0002'),  -- 2 : เล่นเกิน 2 ชม. -> คืนเงินไม่ได้
(1, LOCALTIMESTAMP - INTERVAL '1 day',   349, '111-1-11111-1', 'PAY-SEED-0003');  -- 3 : คืนเงินได้

INSERT INTO order_items (order_id, game_id, price, is_refunded) VALUES
(1, 6, 129, false),   -- 1 Block Logic
(1, 9, 399, false),   -- 2 Dungeon Delvers
(2, 2, 990, false),   -- 3 Kingdoms of Ash
(3, 4, 349, false);   -- 4 Farmstead Days

INSERT INTO library_items (user_id, game_id, order_item_id, acquired_at, playtime_minutes, last_played_at) VALUES
(1, 6, 1, LOCALTIMESTAMP - INTERVAL '20 days', 45,  LOCALTIMESTAMP - INTERVAL '18 days'),
(1, 9, 2, LOCALTIMESTAMP - INTERVAL '20 days', 610, LOCALTIMESTAMP - INTERVAL '2 days'),
(1, 2, 3, LOCALTIMESTAMP - INTERVAL '3 days',  300, LOCALTIMESTAMP - INTERVAL '1 day'),
(1, 4, 4, LOCALTIMESTAMP - INTERVAL '1 day',   30,  LOCALTIMESTAMP - INTERVAL '20 hours');

-- ของในตะกร้า
INSERT INTO cart_items (user_id, game_id) VALUES
(1, 1),   -- player1 : Starfall Odyssey
(2, 7);   -- player2 : Goal Masters 26 (1,590 บาท แต่บัญชี 222 มีแค่ 300 -> ใช้ทดสอบเงินไม่พอ)

-- คำสั่งนี้ปกติไม่ต้องใช้ จะใช้เฉพาะเมื่อต้องการ reset auto generate sequence
-- ALTER SEQUENCE orders_id_seq RESTART WITH 1;
