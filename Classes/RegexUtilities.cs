using System;
using System.Globalization;
using System.Text.RegularExpressions;

public class RegexUtilities
{
    public static bool IsValidEmail(string email)
    {
        // Discards the request if the string is empty
        if (string.IsNullOrWhiteSpace(email))
            return false;

        // Worst case scenario, the program returns false
        try
        {
            // Normalize the domain. Match a single "@" symbol. 
            // Match one or more "." symbols (at least one is needed for the top-level domain).
            // "$" symbol ends the match at the end of the string.
            // DomnainMapper receives the @ and the domain name to translate Unicode characters outside the US-ASCII character range.
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                  RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examines the domain part of the email and normalizes it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                var domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException e)
        {
            return false;
        }
        catch (ArgumentException e)
        {
            return false;
        }

        try
        {
            //Pattern ^: Begin the match at the start of the string.
            //Pattern(?("): Determine whether the first character is a quotation mark. (?(") is the beginning of an alternation construct.
            //Pattern(?(")(".+? (?< !\\)"@): If the first character is a quotation mark, match a beginning quotation mark followed by at least one occurrence of any character, followed by an ending quotation mark. The ending quotation mark must not be preceded by a backslash character (\). (?<! is the beginning of a zero-width negative lookbehind assertion. The string should conclude with an at sign (@).
            //Pattern | (([0 - 9a - z]: If the first character is not a quotation mark, match any alphabetic character from a to z or A to Z(the comparison is case insensitive), or any numeric character from 0 to 9.
            //Pattern(\.(? !\.)): If the next character is a period, match it. If it is not a period, look ahead to the next character and continue the match. (? !\.) is a zero - width negative lookahead assertion that prevents two consecutive periods from appearing in the local part of an email address.
            //Pattern |[-!#\$%&'\*\+/=\?\^`\{\}\|~\w]: If the next character is not a period, match any word character or one of the following characters: -!#$%&'*+/=?^`{}|~
            //Pattern((\.(? !\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*: Match the alternation pattern (a period followed by a non-period, or one of a number of characters) zero or more times.
            //Pattern @: Match the @ character.
            //Pattern(?<=[0 - 9a - z]): Continue the match if the character that precedes the @ character is A through Z, a through z, or 0 through 9.This pattern defines a zero-width positive lookbehind assertion.
            //Pattern(?(\[): Check whether the character that follows @ is an opening bracket.
            //Pattern(\[(\d{ 1,3}\.){ 3}\d{ 1,3}\]): If it is an opening bracket, match the opening bracket followed by an IP address(four sets of one to three digits, with each set separated by a period) and a closing bracket.
            //Pattern | (([0 - 9a - z][-0 - 9a - z] *[0 - 9a - z] *\.)+: If the character that follows @ is not an opening bracket, match one alphanumeric character with a value of A-Z, a - z, or 0 - 9, followed by zero or more occurrences of a hyphen, followed by zero or one alphanumeric character with a value of A-Z, a - z, or 0 - 9, followed by a period. This pattern can be repeated one or more times, and must be followed by the top - level domain name.
            //Pattern[a - z0 - 9][\-a - z0 - 9]{ 0,22}[a-z0-9])): The top-level domain name must begin and end with an alphanumeric character(a-z, A-Z, and 0-9). It can also include from zero to 22 ASCII characters that are either alphanumeric or hyphens.
            return Regex.IsMatch(email,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }
}