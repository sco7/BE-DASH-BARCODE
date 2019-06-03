//using FontaineVerificationProject.Models;
//using FontaineVerificationProject.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace FontaineVerificationProject.Processers
//{
//    public class VerificationProcessor
//    {
//        //For Processing, Validating, Formating

//        public static bool ProcessAddChassis(Verification verification)
//        {
//            return VerificationRepository.AddChassisToDB(verification);
//        }

//        public static bool ProcessDeleteChassis(Verification verification)
//        {
//            return VerificationRepository.DeleteChassisFromDB(verification);
//        }

//        public static bool ProcessUpdateV1(Verification verification)
//        {
//            return VerificationRepository.UpDateV1toDB(verification);
//        }

//        public static bool ProcessUpdateV2(Verification verification)
//        {
//            return VerificationRepository.UpDateV2toDB(verification);
//        }

//        public static string[] ProcessGetChassis()
//        {
//            return VerificationRepository.GetChassisFromDB();
//        }

//        public static string[] ProcessGetChassisNo(Verification verification)
//        {
//            return VerificationRepository.GetChassisById(verification);
//        }


//    }
//}
