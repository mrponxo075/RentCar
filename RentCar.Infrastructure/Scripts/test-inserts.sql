-- ======================================================
--  DATA TEST SCRIPT
-- ======================================================

-- 🔹 1. Brand
INSERT INTO Brand (BrandName) VALUES
('Toyota'),
('Ford'),
('Tesla'),
('BMW'),
('Volkswagen'),
('Renault'),
('Audi'),
('Peugeot'),
('Kia'),
('Nissan');

-- 🔹 2. FuelType
INSERT INTO FuelType (FuelTypeName, FuelTypeDescription, KilometersAutonomy) VALUES
('Petrol', 'Unleaded gasoline engine', 600),
('Diesel', 'Efficient diesel engine', 800),
('Hybrid', 'Combination of petrol and electric', 950),
('Electric', 'Full electric vehicle', 400);

-- 🔹 3. Model
INSERT INTO Model (ModelName, BrandId, FuelTypeId) VALUES
('Corolla', 1, 1),
('RAV4 Hybrid', 1, 3),
('Mustang', 2, 1),
('Model 3', 3, 4),
('i3', 4, 4),
('Golf TDI', 5, 2),
('Clio', 6, 1),
('A4', 7, 2),
('208', 8, 1),
('Sportage', 9, 3),
('Leaf', 10, 4),
('Yaris', 1, 1),
('Focus', 2, 1),
('Zoe', 6, 4),
('Tiguan', 5, 2);

-- 🔹 4. Currency
INSERT INTO Currency (CurrencyId, CurrencyName, Symbol) VALUES
('EUR', 'Euro', '€'),
('USD', 'US Dollar', '$'),
('GBP', 'British Pound', '£');

-- 🔹 Car (con campo Stock)
INSERT INTO Car (
    ModelId, BrandId, Color,
    PricePerDay, PricePerWeek, PricePerHour, CurrencyId, Stock
) VALUES
(1, 1, 'White', 40.00, 240.00, 8.00, 'EUR', 5),
(2, 1, 'Blue', 55.00, 330.00, 10.00, 'EUR', 4),
(3, 2, 'Red', 70.00, 420.00, 12.00, 'EUR', 3),
(4, 3, 'Silver', 90.00, 540.00, 15.00, 'EUR', 6),
(5, 4, 'Black', 80.00, 480.00, 14.00, 'EUR', 5),
(6, 5, 'Grey', 50.00, 300.00, 9.00, 'EUR', 8),
(7, 6, 'Yellow', 35.00, 210.00, 7.00, 'EUR', 7),
(8, 7, 'White', 75.00, 450.00, 12.00, 'EUR', 4),
(9, 8, 'Blue', 45.00, 270.00, 8.00, 'EUR', 9),
(10, 9, 'Black', 60.00, 360.00, 10.00, 'EUR', 6),
(11, 10, 'Green', 65.00, 390.00, 11.00, 'EUR', 5),
(12, 1, 'Red', 42.00, 250.00, 8.00, 'EUR', 4),
(13, 2, 'Grey', 50.00, 300.00, 9.00, 'EUR', 7),
(14, 6, 'White', 55.00, 330.00, 10.00, 'EUR', 5),
(15, 5, 'Silver', 70.00, 420.00, 12.00, 'EUR', 6);


-- 🔹 6. IDCardType
INSERT INTO IDCardType (TypeName, IDCardTypeDescription) VALUES
('DNI', 'Documento Nacional de Identidad'),
('Passport', 'Official passport for international travel'),
('DriverLicense', 'Official driving license');

-- 🔹 7. Customer
INSERT INTO Customer (FirstName, LastName, SecondLastName, Email, Phone, CustomerAddress, IDNumber, IDCardTypeId) VALUES
('Carlos', 'Pérez', 'Gómez', 'carlos.perez@example.com', '600123456', 'Calle Mayor 12, Madrid', '12345678A', 1),
('Laura', 'Martínez', NULL, 'laura.martinez@example.com', '600987654', 'Avenida del Sol 45, Sevilla', 'Y1234567', 2),
('David', 'López', 'Santos', 'david.lopez@example.com', '611223344', 'Calle Luna 5, Valencia', '87654321B', 1),
('María', 'García', 'Ruiz', 'maria.garcia@example.com', '622334455', 'Calle Real 9, Granada', '23456789C', 1),
('Jorge', 'Navarro', NULL, 'jorge.navarro@example.com', '633445566', 'Avenida Atlántico 3, A Coruña', '34567890D', 2),
('Sofía', 'Hernández', 'Martín', 'sofia.hernandez@example.com', '644556677', 'Calle Sol 27, Málaga', '45678901E', 1),
('Pedro', 'Gómez', 'Lozano', 'pedro.gomez@example.com', '655667788', 'Plaza Mayor 14, Valladolid', '56789012F', 3),
('Elena', 'Rodríguez', 'Fernández', 'elena.rodriguez@example.com', '666778899', 'Camino Verde 21, León', '67890123G', 1),
('Antonio', 'Serrano', NULL, 'antonio.serrano@example.com', '677889900', 'Calle Mayor 44, Alicante', '78901234H', 1),
('Lucía', 'Domínguez', 'Morales', 'lucia.dominguez@example.com', '688990011', 'Paseo Marítimo 8, Cádiz', '89012345J', 2);

-- 🔹 8. Tax
INSERT INTO Tax (TaxName, TaxDescription, Rate) VALUES
('IVA General', 'Impuesto al Valor Añadido (21%)', 21.00),
('IVA Reducido', 'Impuesto reducido (10%)', 10.00);

-- 🔹 9. Rental
INSERT INTO Rental (CarId, CustomerId, TaxId, CurrencyId, LicensePlate, StartDate, EndDate, RentalPrice, AssurancePrice) VALUES
(1, 1, 1, 'EUR', '1234ABC', '2025-11-01 09:00:00', '2025-11-05 10:00:00', 160.00, 20.00),
(2, 2, 1, 'EUR', '5678DEF', '2025-10-10 08:00:00', '2025-10-15 09:30:00', 275.00, 25.00),
(3, 3, 1, 'EUR', '9999ZZZ', '2025-09-05 10:00:00', '2025-09-10 12:00:00', 350.00, 30.00),
(4, 4, 1, 'EUR', '1122TES', '2025-08-20 09:00:00', '2025-08-22 09:00:00', 180.00, 15.00),
(5, 5, 1, 'EUR', '3344BMW', '2025-07-15 08:00:00', '2025-07-20 08:00:00', 400.00, 35.00),
(6, 6, 2, 'EUR', '5566VW', '2025-11-01 10:00:00', '2025-11-04 09:00:00', 150.00, 12.00),
(7, 7, 1, 'EUR', '7788REN', '2025-10-05 09:00:00', '2025-10-10 09:00:00', 175.00, 15.00),
(8, 8, 1, 'EUR', '8899AUD', '2025-09-01 10:00:00', '2025-09-03 08:00:00', 150.00, 10.00),
(9, 9, 1, 'EUR', '9911PEU', '2025-08-10 08:00:00', '2025-08-15 09:00:00', 225.00, 20.00),
(10, 10, 1, 'EUR', '1111KIA', '2025-07-25 09:00:00', '2025-07-30 09:00:00', 300.00, 25.00),
(11, 1, 1, 'EUR', '1223ABN', '2025-11-01 09:00:00', '2025-11-03 09:00:00', 130.00, 10.00),
(12, 2, 2, 'EUR', '5655DEB', '2025-10-10 10:00:00', '2025-10-14 08:00:00', 168.00, 15.00),
(13, 3, 1, 'EUR', '947ZZV', '2025-09-12 09:00:00', '2025-09-17 09:00:00', 250.00, 20.00),
(14, 4, 1, 'EUR', '1442TEX', '2025-08-05 08:00:00', '2025-08-08 10:00:00', 165.00, 10.00),
(15, 5, 1, 'EUR', '3354BMZ', '2025-07-01 09:00:00', '2025-07-06 09:00:00', 350.00, 30.00);
