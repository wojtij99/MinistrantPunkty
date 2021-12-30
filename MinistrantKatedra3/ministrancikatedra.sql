-- phpMyAdmin SQL Dump
-- version 5.0.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Czas generowania: 30 Gru 2021, 20:25
-- Wersja serwera: 10.4.17-MariaDB
-- Wersja PHP: 7.3.27

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Baza danych: `ministrancikatedra`
--

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `constevents`
--

CREATE TABLE `constevents` (
  `ID` int(11) NOT NULL,
  `DayOfWeek` text COLLATE utf8_polish_ci NOT NULL,
  `Hour` time NOT NULL,
  `pkt` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Zrzut danych tabeli `constevents`
--

INSERT INTO `constevents` (`ID`, `DayOfWeek`, `Hour`, `pkt`) VALUES
(2, 'Poniedziałek', '06:30:00', 3),
(3, 'Poniedziałek', '07:00:00', 3),
(4, 'Poniedziałek', '18:00:00', 2),
(5, 'Wtorek', '06:30:00', 3),
(6, 'Wtorek', '07:00:00', 3),
(7, 'Wtorek', '18:00:00', 1),
(8, 'Środa', '06:30:00', 3),
(9, 'Środa', '07:00:00', 3),
(10, 'Środa', '18:00:00', 2),
(11, 'Czwartek', '06:30:00', 3),
(12, 'Czwartek', '07:00:00', 3),
(13, 'Czwartek', '18:00:00', 2),
(14, 'Piątek', '06:30:00', 3),
(15, 'Piątek', '07:00:00', 3),
(16, 'Piątek', '18:00:00', 2),
(17, 'Sobota', '06:30:00', 3),
(18, 'Sobota', '07:00:00', 3),
(19, 'Sobota', '18:00:00', 3),
(20, 'Niedziela', '07:00:00', 5),
(21, 'Niedziela', '09:00:00', 5),
(22, 'Niedziela', '10:30:00', 5),
(23, 'Niedziela', '12:00:00', 5),
(24, 'Niedziela', '15:00:00', 5),
(25, 'Niedziela', '17:00:00', 5),
(26, 'Niedziela', '19:00:00', 5),
(27, 'Niedziela', '19:00:00', 3);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `events`
--

CREATE TABLE `events` (
  `ID` int(11) NOT NULL,
  `Punkty` int(11) NOT NULL,
  `data` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `historia`
--

CREATE TABLE `historia` (
  `ID` int(11) NOT NULL,
  `ID_ministrant` int(11) NOT NULL,
  `Punkty` int(11) NOT NULL,
  `Opis` text COLLATE utf8_polish_ci NOT NULL,
  `Data` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `ministranci`
--

CREATE TABLE `ministranci` (
  `ID` int(11) NOT NULL,
  `Imie` text COLLATE utf8_polish_ci NOT NULL,
  `Nazwisko` text COLLATE utf8_polish_ci NOT NULL,
  `haslo` text COLLATE utf8_polish_ci NOT NULL,
  `Obowiazkowe` text COLLATE utf8_polish_ci NOT NULL,
  `Wakacyjne` text COLLATE utf8_polish_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Zrzut danych tabeli `ministranci`
--

INSERT INTO `ministranci` (`ID`, `Imie`, `Nazwisko`, `haslo`, `Obowiazkowe`, `Wakacyjne`) VALUES
(1, 'Kuba', 'Ślęczkowski', '123', 'Czwartek-6:30|Czwartek-7:00', '16-6|17-6|20-6.12:00|'),
(2, 'Adam', 'Sowa', '321', 'Czwartek-18:00|Niedziela-9:00|', '');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `techniczna`
--

CREATE TABLE `techniczna` (
  `ID` int(11) NOT NULL,
  `Name` text COLLATE utf8_polish_ci NOT NULL,
  `V1` text COLLATE utf8_polish_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Zrzut danych tabeli `techniczna`
--

INSERT INTO `techniczna` (`ID`, `Name`, `V1`) VALUES
(1, 'Niedziela', '6'),
(2, 'Haslo', 'qaz'),
(3, 'Setoff', ''),
(4, 'nieobecność', '2021-10-17 00:16:42');

--
-- Indeksy dla zrzutów tabel
--

--
-- Indeksy dla tabeli `constevents`
--
ALTER TABLE `constevents`
  ADD PRIMARY KEY (`ID`);

--
-- Indeksy dla tabeli `events`
--
ALTER TABLE `events`
  ADD PRIMARY KEY (`ID`);

--
-- Indeksy dla tabeli `historia`
--
ALTER TABLE `historia`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `historiaIDministrant` (`ID_ministrant`);

--
-- Indeksy dla tabeli `ministranci`
--
ALTER TABLE `ministranci`
  ADD PRIMARY KEY (`ID`);

--
-- Indeksy dla tabeli `techniczna`
--
ALTER TABLE `techniczna`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT dla zrzuconych tabel
--

--
-- AUTO_INCREMENT dla tabeli `constevents`
--
ALTER TABLE `constevents`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;

--
-- AUTO_INCREMENT dla tabeli `events`
--
ALTER TABLE `events`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT dla tabeli `historia`
--
ALTER TABLE `historia`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT dla tabeli `ministranci`
--
ALTER TABLE `ministranci`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT dla tabeli `techniczna`
--
ALTER TABLE `techniczna`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- Ograniczenia dla zrzutów tabel
--

--
-- Ograniczenia dla tabeli `historia`
--
ALTER TABLE `historia`
  ADD CONSTRAINT `historiaIDministrant` FOREIGN KEY (`ID_ministrant`) REFERENCES `ministranci` (`ID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
