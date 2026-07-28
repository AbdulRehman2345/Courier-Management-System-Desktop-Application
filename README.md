# FastR CMS — Courier Management System (Desktop Application)

A Windows desktop application for managing courier and parcel operations, built with C# Windows Forms and backed by a MySQL database. This is the desktop counterpart to a full courier management workflow — covering staff-side parcel operations and a user-facing parcel tracking panel, all within a native Windows application.

## Overview & System Flow

The system is structured around two core panels — a **Staff Panel** for internal parcel operations and a **User Panel** for public parcel tracking — connected through a shared MySQL database. The flowchart below illustrates how a parcel moves through the system, from booking to final delivery status.

![System Flowchart](flowchart.jpg)

## Preview

![Login Screen](login.jpg)

![Staff Dashboard](dashboard.jpg)

## Features

### Authentication
- Secure login with separate sign-in paths for Staff and Users
- User registration (sign-up) with first name, last name, email, contact number, and password

### Staff Panel
- **Dashboard** — live overview of parcel counts by status: Total Parcels, Collected, Picked Up, Shipped, Out for Delivery, Arrived at Destination, Accepted by Courier, Delivered, and Unsuccessful Delivery
- **Parcel Details & Management** — insert, search, reset, and delete parcel records, including sender and receiver details, addresses, contact numbers, charges, and delivery date
- **Parcel Status Management** — look up a parcel by tracking number and update its current delivery status
- **Parcel Reporting** — generate and view parcel reports filtered by date range and parcel status

### User Panel
- Track a parcel in real time using its tracking number
- Print parcel status details directly from the application

## Built With

<p>
  <img src="https://skillicons.dev/icons?i=cs,dotnet,mysql,visualstudio" />
</p>

- **C#** — Core application logic
- **Windows Forms (.NET Framework 4.7.2)** — Desktop UI across all panels and forms
- **MySQL** — Backend relational database (`fastr_db`)
- **MySql.Data** — Official MySQL connector for .NET, handling all database communication
- **Visual Studio** — Development environment and solution structure

## Project Structure
