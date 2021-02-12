﻿using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string CarAdded = "New Car Succesfully Added";
        public static string CarNameInvalid = "The car daily price  must be greater than 0 and  name must be at least 2 characters ";
        public static string MaintenanceTime = "System is under maintenance";
        public static string CarsListed = "Listed";
        public static string CarUpdated = "Car Updated";
        public static string CarDeleted = "Car Deleted";
        public static string CarDetails = "Details were brought";
        public static string CustomerAdded = "Customer Added";
        public static string CustomerNameInvalid= "name must be at least 2 characters";
        public static string CustomerDeleted="Customer Deleted";
        public static string CustomerListed="Customers Listed";
        public static string CustomerUpdated="Customer Updated";
        public static string CustomerUnUpdated="customer couldnt update";
        public static string RentalUpdated="Rental Updated";
        public static string RentalAdded = "Rental Added";
        public static string RentalDeleted = "Rental Deleted";
        public static string RentalListed = "Rentals Listed";
        public static string CarInUse="Currently Not Avaliable for Rent";
        public static string NoRecordsFound="No Record";
        public static string UserAdded = "User Added";
        public static string UserDeleted = "User Deleted";
        public static string UsersListed = "User Listed";
        public static string UserUpdated="User Updated";
        public static string UserNameInvalid = "name must be at least 2 characters";
    }
}