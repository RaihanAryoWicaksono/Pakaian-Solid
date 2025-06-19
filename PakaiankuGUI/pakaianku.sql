-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Waktu pembuatan: 19 Jun 2025 pada 21.49
-- Versi server: 10.4.32-MariaDB
-- Versi PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `pakaianku`
--

-- --------------------------------------------------------

--
-- Struktur dari tabel `keranjang`
--

CREATE TABLE `keranjang` (
  `Id` int(11) NOT NULL,
  `KodePakaian` varchar(50) NOT NULL,
  `Quantity` int(11) NOT NULL DEFAULT 1,
  `TanggalDitambahkan` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struktur dari tabel `pakaian`
--

CREATE TABLE `pakaian` (
  `Kode` varchar(50) NOT NULL,
  `Nama` varchar(255) NOT NULL,
  `Kategori` varchar(100) NOT NULL,
  `Warna` varchar(50) NOT NULL,
  `Ukuran` varchar(20) NOT NULL,
  `Harga` decimal(10,2) NOT NULL DEFAULT 0.00,
  `Stok` int(11) NOT NULL DEFAULT 0,
  `Status` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data untuk tabel `pakaian`
--

INSERT INTO `pakaian` (`Kode`, `Nama`, `Kategori`, `Warna`, `Ukuran`, `Harga`, `Stok`, `Status`) VALUES
('CL001', 'Celana Jeans', 'Celana', 'Biru', '32', 350000.00, 19, 'Tersedia'),
('CL002', 'Celana Chino', 'Celana', 'Khaki', '30', 320000.00, 19, 'Tersedia'),
('CL003', 'Celana Pendek', 'Celana', 'Hitam', '34', 180000.00, 19, 'Tersedia'),
('e', 'e', 'e', 'e', 'e', 400000.00, 20, 'Tersedia'),
('JK001', 'Jaket Bomber', 'Jaket', 'Hitam', 'L', 450000.00, 20, 'Tersedia'),
('JK002', 'Jaket Denim', 'Jaket', 'Biru', 'M', 480000.00, 20, 'Tersedia'),
('JK003', 'Jaket Hoodie', 'Jaket', 'Abu-abu', 'XL', 375000.00, 7, 'Tersedia'),
('k', 'k', 'k', 'k', 'k', 122000.00, 2, 'Tersedia'),
('KM001', 'Kemeja Formal Pria', 'Kemeja', 'Putih', 'L', 250000.00, 10, 'Tersedia'),
('KM002', 'Kemeja Formal Pria', 'Kemeja', 'Biru', 'M', 245000.00, 8, 'Tersedia'),
('KM003', 'Kemeja Formal Pria', 'Kemeja', 'Hitam', 'XL', 255000.00, 5, 'Tersedia'),
('KS001', 'Kaos Premium', 'Kaos', 'Hitam', 'M', 150000.00, 15, '0'),
('KS002', 'Kaos Premium', 'Kaos', 'Putih', 'L', 155000.00, 12, '0'),
('KS003', 'Kaos Grafis', 'Kaos', 'Merah', 'M', 180000.00, 7, '0'),
('ku200', 'a', 'a', 'a', 'a', 350000.00, 20, 'Tersedia'),
('o', 'o', 'o', 'o', 'o', 340000.00, 12, 'Tersedia'),
('r', 'r', 'r', 'r', 'r', 200000.00, 2, 'Tersedia'),
('z', 'z', 'z', 'z', 'z', 400000.00, 30, 'Tersedia');

-- --------------------------------------------------------

--
-- Struktur dari tabel `users`
--

CREATE TABLE `users` (
  `Id` int(11) NOT NULL,
  `Username` varchar(255) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `Role` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data untuk tabel `users`
--

INSERT INTO `users` (`Id`, `Username`, `Password`, `Role`) VALUES
(1, 'admin', 'admin123', 'Admin'),
(2, 'customer1', 'customerpassword', 'Customer'),
(3, 'dimas', '123', 'Admin'),
(4, 'rudi', '321', 'Customer'),
(5, 'testing', '123456', 'Customer'),
(6, 'asu', 'temempik', 'Customer'),
(7, 'apalah', '123456', 'Customer'),
(8, 'apa123', '12345678', 'Customer'),
(9, 'maulanal', '12345678', 'Customer');

-- --------------------------------------------------------

--
-- Struktur dari tabel `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Indexes for dumped tables
--

--
-- Indeks untuk tabel `keranjang`
--
ALTER TABLE `keranjang`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `KodePakaian` (`KodePakaian`);

--
-- Indeks untuk tabel `pakaian`
--
ALTER TABLE `pakaian`
  ADD PRIMARY KEY (`Kode`);

--
-- Indeks untuk tabel `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Username` (`Username`);

--
-- Indeks untuk tabel `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT untuk tabel yang dibuang
--

--
-- AUTO_INCREMENT untuk tabel `keranjang`
--
ALTER TABLE `keranjang`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT untuk tabel `users`
--
ALTER TABLE `users`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- Ketidakleluasaan untuk tabel pelimpahan (Dumped Tables)
--

--
-- Ketidakleluasaan untuk tabel `keranjang`
--
ALTER TABLE `keranjang`
  ADD CONSTRAINT `keranjang_ibfk_1` FOREIGN KEY (`KodePakaian`) REFERENCES `pakaian` (`Kode`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
