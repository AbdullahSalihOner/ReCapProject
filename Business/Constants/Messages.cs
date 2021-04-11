using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
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
        internal static string BrandAdded;
        internal static string BrandDeleted;
        internal static string BrandUpdated;
        internal static string ColorDeleted;
        internal static string ColorAdded;
        internal static string ColorUpdated;
        internal static string CarImagesListed;
        internal static string CarCountOfCategoryError;
        internal static string CarNameAlreadyExists;
        internal static string CategoryLimitExceded;
        

        public static string AuthorizationDenied = "Yetkiniz yok";

        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Sisteme giriş başarılı";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";



        public static string CarImageAdded;
        public static string CarImageUpdated;
        public static string CarImageDeleted;
        public static string CountOfPictureError = "Fazla fotoraf";
        internal static string RentalUndeliveredCar;
        internal static string RentalNotAvailable;
        internal static List<CarDetailDto> CarNotFound;
        internal static string CardAdded;
        internal static string CardDeleted;
        internal static string CardUpdated;
        internal static string FindexZero;
        internal static string InsufficientScore;
        internal static string SuccessfulPaid;
        public static string AddSingular = " has been added.";
        public static string UpdateSingular = " has been updated.";
        public static string DeleteSingular = " has been deleted.";
        public static string NotExist = "There is no such a ";
        public static string NotEngouhFindex = "Your Findeks points are not enough to rent this car.";
        public static string EngouhFindex = "Your Findeks points are enough to rent this car";
    }
}
