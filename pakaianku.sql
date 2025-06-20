-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               8.0.30 - MySQL Community Server - GPL
-- Server OS:                    Win64
-- HeidiSQL Version:             12.1.0.6537
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

-- Dumping data for table pakaianku.keranjangbelanja: ~0 rows (approximately)
REPLACE INTO `keranjangbelanja` (`Id`, `UserId`, `CreatedAt`, `UpdatedAt`) VALUES
	(1, 4, '2025-06-19 13:57:45', '2025-06-19 15:54:08');

-- Dumping data for table pakaianku.keranjangitems: ~1 rows (approximately)

-- Dumping data for table pakaianku.pakaian: ~14 rows (approximately)
REPLACE INTO `pakaian` (`Kode`, `Nama`, `Kategori`, `Warna`, `Ukuran`, `Harga`, `Stok`, `Status`) VALUES
	('CL001', 'Celana Jeans Cihuy', 'Celana', 'Biru', '32', 350000.00, 13, 'Dipesan'),
	('CL002', 'Celana Chino', 'Celana', 'Khaki', '30', 320000.00, 2, 'Dipesan'),
	('JK001', 'Jaket Bomber', 'Jaket', 'Hitam', 'L', 450000.00, 4, 'Tersedia'),
	('JK003', 'Jaket Hoodie', 'Jaket', 'Abu-abu', 'XL', 375000.00, 6, 'Tersedia'),
	('KM001', 'Kemeja Formal Pria', 'Kemeja', 'Putih', 'L', 250000.00, 9, 'Tersedia'),
	('KM002', 'Kemeja Formal Pria', 'Kemeja', 'Biru', 'M', 245000.00, 6, 'Dipesan'),
	('KM003', 'Kemeja Formal Pria', 'Kemeja', 'Hitam', 'XL', 255000.00, 4, 'DalamKeranjang'),
	('KM007', 'Kasda', 'Kemeja', 'Merak Kotak', 'XLL', 220000.00, 2147483646, 'DalamKeranjang'),
	('KM009', 'asdas', 'kemeja', 'do', 'L', 290000.00, 2147483646, 'DalamKeranjang'),
	('KS001', 'Kaos Premium', 'Kaos', 'Hitam', 'M', 150000.00, 14, 'DalamKeranjang'),
	('KS002', 'Kaos Premium', 'Kaos', 'Putih', 'L', 155000.00, 11, 'DalamKeranjang'),
	('KS003', 'Kaos Grafis', 'Kaos', 'Merah', 'M', 180000.00, 6, 'DalamKeranjang');

-- Dumping data for table pakaianku.users: ~4 rows (approximately)
REPLACE INTO `users` (`Id`, `Username`, `Password`, `Role`) VALUES
	(1, 'admin', 'admin123', 'Admin'),
	(2, 'customer1', 'customerpassword', 'Customer'),
	(3, 'dimas', '123', 'Admin'),
	(4, 'rudi', '321', 'Customer'),
	(5, 'Cihuy', '123', 'Customer'),
	(7, 'digidaw', '123', 'Customer'),
	(9, 'maul', '321', 'Customer'),
	(11, 'owed', '1', 'Customer'),
	(12, 'hihi', '1232', 'Customer'),
	(14, 'owed2', '2', 'Customer'),
	(15, 'eror', '2', 'Customer');

-- Dumping data for table pakaianku.__efmigrationshistory: ~0 rows (approximately)

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
