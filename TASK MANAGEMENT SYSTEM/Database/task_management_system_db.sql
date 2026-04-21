-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 20, 2026 at 10:41 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `task_management_system_db`
CREATE DATABASE /*!32312 IF NOT EXISTS*/`task_management_system_db` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;

USE `task_management_system_db`;

-- --------------------------------------------------------

--
-- Table structure for table `projects`
--

CREATE TABLE `projects` (
  `id` int(255) NOT NULL,
  `name` varchar(255) NOT NULL,
  `description` varchar(255) NOT NULL,
  `start_date` date NOT NULL,
  `due_date` date NOT NULL,
  `status` enum('Started','Ongoing','Finished','Missed') NOT NULL,
  `total_tasks` int(11) DEFAULT NULL,
  `is_archived` int(11) DEFAULT 0,
  `progress` double(10,0) DEFAULT 0,
  `finished_tasks` int(11) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `projects`
--

INSERT INTO `projects` (`id`, `name`, `description`, `start_date`, `due_date`, `status`, `total_tasks`, `is_archived`, `progress`, `finished_tasks`) VALUES
(1, 'Sample Project 1', 'Sample Project Descriptions', '2023-12-04', '2023-12-30', 'Missed', 3, 0, 0, 0),
(2, 'Sample Project 2', 'Sample Project Description\nSample Project Description', '2023-12-04', '2023-12-30', 'Missed', 3, 0, 67, 2),
(3, 'Sample Project 3', 'Sample Project Description', '2023-12-04', '2024-01-01', 'Finished', 3, 0, 100, 3),
(4, 'Sample Project 4', 'Sample Project Description\nSample Project Description', '2023-12-04', '2023-12-04', 'Missed', 3, 0, 33, 1);

-- --------------------------------------------------------

--
-- Table structure for table `tasks`
--

CREATE TABLE `tasks` (
  `id` int(255) NOT NULL,
  `project_id` int(255) NOT NULL,
  `project_name` varchar(50) NOT NULL,
  `name` varchar(255) NOT NULL,
  `description` varchar(255) DEFAULT NULL,
  `start_date` date NOT NULL DEFAULT current_timestamp(),
  `due_date` date NOT NULL,
  `status` enum('Pending','Finished','Missed') NOT NULL,
  `is_archived` int(11) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tasks`
--

INSERT INTO `tasks` (`id`, `project_id`, `project_name`, `name`, `description`, `start_date`, `due_date`, `status`, `is_archived`) VALUES
(1, 1, '', 'Sample Task 1', 'Sample Task Description', '2023-12-04', '2023-12-30', 'Missed', 0),
(2, 1, '', 'Sample Task 2', 'Sample Task Description', '2023-12-04', '2023-12-30', 'Missed', 0),
(3, 1, '', 'Sample Task 3', 'Sample Task Description', '2023-12-04', '2023-12-30', 'Missed', 0),
(4, 2, '', 'Sample Task 1', 'Sample Task Description', '2026-04-20', '2023-12-04', 'Finished', 0),
(5, 2, '', 'Sample Task 2', 'Sample Task Description', '2026-04-20', '2023-12-04', 'Missed', 0),
(6, 3, '', 'Sample Task 1', 'Sample Task Description', '2026-04-20', '2024-01-01', 'Finished', 0),
(7, 3, '', 'Sample Task 2', 'Sample Task Description', '2026-04-20', '2024-01-01', 'Finished', 0),
(8, 4, '', 'Sample Task 1', 'Sample Task Description', '2026-04-20', '2023-12-04', 'Missed', 0),
(10, 2, '', 'Sample Task 3', 'Sample Task Description', '2026-04-20', '2023-12-30', 'Finished', 0),
(11, 3, '', 'Sample Task 3', 'Sample Task Description', '2026-04-20', '2024-01-01', 'Finished', 0),
(12, 4, '', 'Sample Task 2', 'Sample Task Description', '2026-04-20', '2023-12-04', 'Finished', 0),
(13, 4, '', 'Sample Task 3', 'Sample Task Description', '2026-04-20', '2023-12-04', 'Missed', 0);

-- --------------------------------------------------------

--
-- Table structure for table `task_users`
--

CREATE TABLE `task_users` (
  `id` int(11) NOT NULL,
  `task_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `status` enum('Pending','Finished','Missed') NOT NULL DEFAULT 'Pending',
  `is_archived` int(11) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` int(255) NOT NULL,
  `first_name` varchar(255) NOT NULL,
  `middle_name` varchar(255) NOT NULL,
  `last_name` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `date_joined` date NOT NULL DEFAULT current_timestamp(),
  `tasks_finished` int(11) NOT NULL DEFAULT 0,
  `is_superuser` tinyint(1) NOT NULL,
  `is_archived` int(11) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `first_name`, `middle_name`, `last_name`, `email`, `password`, `date_joined`, `tasks_finished`, `is_superuser`, `is_archived`) VALUES
(1, 'admin', 'admin', 'admin', 'admin', 'admin', '2023-12-04', 0, 1, 0),
(2, 'user', 'user', 'user', 'user', 'user', '2023-12-04', 0, 0, 0);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `projects`
--
ALTER TABLE `projects`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `UNIQUE` (`name`);

--
-- Indexes for table `tasks`
--
ALTER TABLE `tasks`
  ADD PRIMARY KEY (`id`),
  ADD KEY `FK_ProjectID` (`project_id`);

--
-- Indexes for table `task_users`
--
ALTER TABLE `task_users`
  ADD PRIMARY KEY (`id`),
  ADD KEY `FK_task_id` (`task_id`),
  ADD KEY `FK_user_id` (`user_id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `projects`
--
ALTER TABLE `projects`
  MODIFY `id` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `tasks`
--
ALTER TABLE `tasks`
  MODIFY `id` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT for table `task_users`
--
ALTER TABLE `task_users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `tasks`
--
ALTER TABLE `tasks`
  ADD CONSTRAINT `FK_ProjectID` FOREIGN KEY (`project_id`) REFERENCES `projects` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `task_users`
--
ALTER TABLE `task_users`
  ADD CONSTRAINT `FK_task_id` FOREIGN KEY (`task_id`) REFERENCES `tasks` (`id`),
  ADD CONSTRAINT `FK_user_id` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
