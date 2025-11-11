-- ======================================================
--  CREATE TABLES SCRIPT
-- ======================================================

-- 🔹 1. Brand
CREATE TABLE Brand (
    BrandId INT IDENTITY(1,1) CONSTRAINT PK_Brand PRIMARY KEY,
    BrandName NVARCHAR(50) NOT NULL
);

-- 🔹 2. FuelType
CREATE TABLE FuelType (
    FuelTypeId INT IDENTITY(1,1) CONSTRAINT PK_FuelType PRIMARY KEY,
    FuelTypeName NVARCHAR(50) NOT NULL,
    FuelTypeDescription NVARCHAR(255) NULL,
    KilometersAutonomy SMALLINT NOT NULL
        CONSTRAINT CK_FuelType_KilometersAutonomy_Positive CHECK (KilometersAutonomy >= 0)
);

-- 🔹 3. Model
CREATE TABLE Model (
    ModelId INT IDENTITY(1,1) CONSTRAINT PK_Model PRIMARY KEY,
    ModelName NVARCHAR(100) NOT NULL,
    BrandId INT NOT NULL,
    FuelTypeId INT NOT NULL,
    CONSTRAINT FK_Model_Brand FOREIGN KEY (BrandId) REFERENCES Brand(BrandId),
    CONSTRAINT FK_Model_FuelType FOREIGN KEY (FuelTypeId) REFERENCES FuelType(FuelTypeId)
);

-- 🔹 4. Currency
CREATE TABLE Currency (
    CurrencyId CHAR(3) CONSTRAINT PK_Currency PRIMARY KEY,
    CurrencyName NVARCHAR(50) NOT NULL,
    Symbol NVARCHAR(3) NULL
);

-- 🔹 5. Car
CREATE TABLE Car (
    CarId INT IDENTITY(1,1) CONSTRAINT PK_Car PRIMARY KEY,
    ModelId INT UNIQUE NOT NULL,
    BrandId INT NOT NULL,
    Color NVARCHAR(50) NOT NULL,
    PricePerDay DECIMAL(10,2) NOT NULL
        CONSTRAINT CK_Car_PricePerDay_Positive CHECK (PricePerDay >= 0),
    PricePerWeek DECIMAL(10,2) NOT NULL
        CONSTRAINT CK_Car_PricePerWeek_Positive CHECK (PricePerWeek >= 0),
    PricePerHour DECIMAL(10,2) NOT NULL
        CONSTRAINT CK_Car_PricePerHour_Positive CHECK (PricePerHour >= 0),
    CurrencyId CHAR(3) NOT NULL,
    Stock SMALLINT NOT NULL
        CONSTRAINT CK_Car_Stock_Positive CHECK (Stock >= 0),
    CONSTRAINT FK_Car_Model FOREIGN KEY (ModelId) REFERENCES Model(ModelId),
    CONSTRAINT FK_Car_Brand FOREIGN KEY (BrandId) REFERENCES Brand(BrandId),
    CONSTRAINT FK_Car_Currency FOREIGN KEY (CurrencyId) REFERENCES Currency(CurrencyId)
);

-- 🔹 6. IDCardType
CREATE TABLE IDCardType (
    IDCardTypeId INT IDENTITY(1,1) CONSTRAINT PK_IDCardType PRIMARY KEY,
    TypeName NVARCHAR(50) NOT NULL,
    IDCardTypeDescription NVARCHAR(255) NULL
);

-- 🔹 7. Customer
CREATE TABLE Customer (
    CustomerId INT IDENTITY(1,1) CONSTRAINT PK_Customer PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    SecondLastName NVARCHAR(50) NULL,
    Email NVARCHAR(150) NULL,
    Phone VARCHAR(20) NOT NULL,
    CustomerAddress NVARCHAR(255) NOT NULL,
    IDNumber NVARCHAR(50) NULL,
    IDCardTypeId INT NULL,
    CONSTRAINT FK_Customer_IDCardType FOREIGN KEY (IDCardTypeId) REFERENCES IDCardType(IDCardTypeId)
);

-- 🔹 8. Tax
CREATE TABLE Tax (
    TaxId INT IDENTITY(1,1) CONSTRAINT PK_Tax PRIMARY KEY,
    TaxName NVARCHAR(100) NOT NULL,
    TaxDescription NVARCHAR(255) NULL,
    Rate DECIMAL(5,2) NOT NULL
        CONSTRAINT CK_Tax_Rate_Positive CHECK (Rate >= 0 AND Rate <= 100)
);

-- 🔹 9. Rental
CREATE TABLE Rental (
    RentalId INT IDENTITY(1,1) CONSTRAINT PK_Rental PRIMARY KEY,
    CarId INT NOT NULL,
    CustomerId INT NOT NULL,
    TaxId INT NOT NULL,
    CurrencyId CHAR(3) NOT NULL,
    LicensePlate VARCHAR(20) NOT NULL,
    StartDate DATETIME2(0) NOT NULL,
    EndDate DATETIME2(0) NOT NULL,
    RentalPrice DECIMAL(10,2) NOT NULL
        CONSTRAINT CK_Rental_RentalPrice_Positive CHECK (RentalPrice >= 0),
    AssurancePrice DECIMAL(10,2) NULL
        CONSTRAINT CK_Rental_AssurancePrice_Positive CHECK (AssurancePrice >= 0),
    CONSTRAINT FK_Rental_Car FOREIGN KEY (CarId) REFERENCES Car(CarId),
    CONSTRAINT FK_Rental_Customer FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId),
    CONSTRAINT FK_Rental_Tax FOREIGN KEY (TaxId) REFERENCES Tax(TaxId),
    CONSTRAINT FK_Rental_Currency FOREIGN KEY (CurrencyId) REFERENCES Currency(CurrencyId)
);
