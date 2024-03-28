using System;

namespace LegacyApp.validators;

public class Validator
{
    public static bool validEmail(string email)
    {
        if (!email.Contains("@") && !email.Contains("."))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static bool validName(string firstName, string lastName)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static bool validAge(DateTime dateOfBirth)
    {
        var now = DateTime.Now;
        int age = now.Year - dateOfBirth.Year;
        if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

        if (age < 21)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static bool validCredit(User user)
    {
        if (user.HasCreditLimit && user.CreditLimit < 500)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}