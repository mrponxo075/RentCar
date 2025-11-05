-- 🔹 Drop tables in reverse order of dependencies to avoid foreign key constraint issues.

DROP TABLE IF EXISTS Rental;
DROP TABLE IF EXISTS Customer;
DROP TABLE IF EXISTS IDCardType;
DROP TABLE IF EXISTS Car;
DROP TABLE IF EXISTS Currency;
DROP TABLE IF EXISTS Model;
DROP TABLE IF EXISTS FuelType;
DROP TABLE IF EXISTS Brand;
DROP TABLE IF EXISTS Tax;
