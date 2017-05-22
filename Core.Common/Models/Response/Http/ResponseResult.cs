using System.Net;

namespace Core.Authorization.Common.Models.Response.Http
{
    public static class ResponseResult
    {
        public static ApiDescription Success = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.OK,
                CodeString = "success"
            }
        };

        public static ApiDescription BusinessConflict = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.Conflict,
                CodeString = "business_conflict"
            },
            Description = "Item conflict"
        };

        public static ApiDescription InsufficientFunds = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.Conflict,
                CodeString = "insufficient_funds"
            },
            Description = "Insufficient funds"
        };

        public static ApiDescription SocialAlreadyAttach = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.Conflict,
                CodeString = "social_already attach"
            },
            Description = "Social account already attach"
        };

        public static ApiDescription UserAlreadyExist = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.Conflict,
                CodeString = "user_exist"
            },
            Description = "User already exist"
        };

        public static ApiDescription CredentialWrong = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.NotAcceptable,
                CodeString = "credential_wrong"
            },
            Description = "Credential wrong"
        };

        public static ApiDescription CredentialExistAnotherUser = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.Conflict,
                CodeString = "credential_exist_another_user"
            },
            Description = "Credential already exist"
        };

        public static ApiDescription BadParameters = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.BadRequest,
                CodeString = "bad_parameters"
            },
            Description = "Bad parameters"
        };

        public static ApiDescription PasswordNotSecurity = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.BadRequest,
                CodeString = "password_not_security"
            },
            Description = "Passsword not security"
        };

        public static ApiDescription LessonPackageEnded = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.NotAcceptable,
                CodeString = "package_ended"
            },
            Description = "Package lesson ended"
        };

        public static ApiDescription BookingThisTimeForbidden = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.Conflict,
                CodeString = "booking_time_forbidden"
            },
            Description = "Booking this time forbidden"
        };

        public static ApiDescription FileNotSupported = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.BadRequest,
                CodeString = "file_not_supported"
            },
            Description = "File not supported"
        };

        public static ApiDescription InternalError = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.InternalServerError,
                CodeString = "internal_error"
            }
        };

        public static ApiDescription NotFound = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.NotFound,
                CodeString = "not_found"
            }
        };

        public static ApiDescription MaxSizeLimit = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.BadRequest,
                CodeString = "max_size_error"
            }
        };

        public static ApiDescription FieldInvalid = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.BadRequest,
                CodeString = "common.field_invalid"
            }
        };

        public static ApiDescription FieldNotBlank = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.BadRequest,
                CodeString = "common.field_not_blank"
            },
            Description = "Field not blank"
        };

        public static ApiDescription ExpirateToken = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.Forbidden,
                CodeString = "sec.access_token_invalid"
            },
            Description = "Expirate Token",
            Field = "accesstoken"
        };

        public static ApiDescription NotAccess = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.Forbidden,
                CodeString = "sec.not_access"
            },
            Description = "Not access"
        };

        public static ApiDescription NotAnotherCredential = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.Forbidden,
                CodeString = "not_another_credential"
            },
            Description = "Not another credential for login"
        };

        public static ApiDescription AccountDeactivated = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.Forbidden,
                CodeString = "sec.account_deactivated"
            },
            Description = "Acoount deactivated"
        };

        public static ApiDescription WrongStateException = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.Conflict,
                CodeString = "wrong_state"
            },
            Description = "Wrong state"
        };
        public static ApiDescription DeleteDefaultCard = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.Conflict,
                CodeString = "delete_default_card"
            },
            Description = "Unable to delete the default card"
        };
        public static ApiDescription WrongRole = new ApiDescription
        {
            Code = new ApiCode
            {
                HttpCode = HttpStatusCode.Conflict,
                CodeString = "wrong_role"
            },
            Description = "User wrong role"
        };
    }
}