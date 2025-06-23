# Description
Bill Time is a __WPF__ application that allows freelance developers keep track of their work for clients. 
It allows users to create clients with different pay rate information, management of payments, and make edits to the working records of the developer.
The application has a built in timer so a user can keep record of their work and to auto calculate the hours worked based on the client
pay rate information specified in the application.

<p align="center">
  <img src="https://github.com/Franco-Diaz-Licham/BillTimeApp/assets/138960498/17050f00-d645-4f09-8a8f-12fbc7dc7693" />
</p>

Important word definitions for payment information can be found under the "defaults" section as tooltips. They are also be defined below:

* __Hourly Rate:__ Amount to charge per hour.
* __Pre-Bil:__ Whether the client was charged before work.
* __Cut-off:__ Whether the client has a cut-off time.
* __Cut-off Number:__ Maximum amount of time to work for a client in hours.
* __Minimum Hours:__ Minimum amount of time to work for a client in hours.
* __Billing Increment:__ Period of time to charge client in hours.
* __Round-up X:__ Time threshold to add an additional billing increment in minutes.

The application uses __SQLite__ for the database and data access is implemented with __Dapper__ as the ORM.

