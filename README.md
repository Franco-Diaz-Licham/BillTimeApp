# Description
Bill Time is a WPF application that allows freelance developers keep track of their work for clients. 
It allows users to create clients with different pay rate requirements, management payments, and make edits to the working records.
The application has a built in timer so a user can keep record of their work and to auto calculate the hours based on the client
pay rate information specified in the application.

<p align="center">
  <img src="https://github.com/Franco-Diaz-Licham/BillTimeApp/assets/138960498/17050f00-d645-4f09-8a8f-12fbc7dc7693" />
</p>

Important word definitions for payment information can be found under the "defaults" section as tooltips.
They are also be defined below:

* Hourly Rate: Amount to charge per hour.
* Pre-Bil: Whether the client was charged before work.
* Cut-off: Whether the client has a cut-off time.
* Cut-off Number: Maximum amount of time to work for a client in hours.
* Minimum Hours: Minimum amount of time to work for a client in hours.
* Billing Increment: Period of time to charge client in hours.
* Round-up X: Time threshold to add an additional billing increment in minutes.

The application uses SQLite for the database and data access is implemented with Dapper as the ORM.
This application was completed as a practice project based on Tim Corey's course on C# Application Development.
