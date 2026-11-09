-- =====================================================================
--  Online Game Store : 1) สร้าง ROLE (USER) และ DATABASE
-- ---------------------------------------------------------------------
--  รันด้วยโปรแกรม pgAdmin4
--    1. เปิด Query Tool ของ database "postgres" (login ด้วย user postgres)
--    2. highlight ทีละคำสั่ง แล้วกด Execute (F5) ทีละคำสั่ง
--       (CREATE/DROP DATABASE รันรวมกับคำสั่งอื่นในครั้งเดียวไม่ได้)
--  รันแค่ครั้งเดียว
-- =====================================================================

-------------------------DROP DB and ROLE (USER)------------------
-- ใช้เมื่อต้องการเคลียร์ของเก่าเท่านั้น
-- DROP DATABASE IF EXISTS gamestore;
-- DROP ROLE IF EXISTS gamestore_app;

-------------------------CREATE ROLE (USER)-----------------------
CREATE ROLE gamestore_app LOGIN ENCRYPTED PASSWORD 'GameStore01f2e2fa';

-------------------------CREATE DATABASE-------------------------
CREATE DATABASE gamestore OWNER gamestore_app;

-- ขั้นต่อไป: เปิด Query Tool ของ database "gamestore" แล้วรันไฟล์ 2_create_tables.sql ทั้งไฟล์
