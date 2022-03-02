using System;
using System.Collections.Generic;
using System.Linq;

namespace PatternMatching
{
    public class SwitchStatements
    {
        // Classic patterns
        public string Boolean(bool val)
        {
            return val switch
            {
                true => "true",
                false => "false"
            };
        }

        //Variable patterns
        public string Greeter(string name, bool greetWithName)
        {
            return name switch
            {
                _ when greetWithName == false => "Hi",
                "Buket" => "Hi Buket",
                var str when str.StartsWith("Mrs.") || str.StartsWith("Mr.") => $"Greetings {str}",
                var str => $"Hello {str}"
            };
        }

        //Relational patterns
        public string Relational(int input)
        {
            return input switch
            {
                < 3 => "less than 3",
                <= 7 => "less than or equal to 7",
                < 10 => "less than 10",
                _ => "greater than or equal to 10"
            };
        }

        //Multiple patterns
        public string Multiple(int input)
        {
            return input switch
            {
                1 or 2 or 3 => "1,2 or 3",
                > 3 and <= 6 => "between and 6",
                not 7 => "not 7",
                _ => "7"
            };
        }

        //Tuple patterns
        public string Tuple(int @int, bool @bool)
        {
            return (@int, @bool) switch
            {
                (< 4, true) => "lower than 4 and true",
                (< 4, false) => "lower than 4 and false",
                (4, true) => "4 and true",
                (5, _) => "5 and true or false",
                (_, false) => "any number and false",
                _ => "something else"
            };
        }

        //Property patterns
        public record User(string Name, Role Role);

        public enum Role
        {
            Admin,
            User,
            Manager,
            Developer
        }

        public string Property(User user)
        {
            return user switch
            {
                { Role: Role.Admin } => "the user is an admin",
                { Role: Role.Manager } => "the user is a manager",
                { Name: "Ugur", Role: Role.Developer } => "the is Ugur and he is a developer",
                { Name: "Ugur" } => "the user is Ugur and he isn't a developer :( ",
                _ => "the user is unknown"
            };
        }

        //Nested Property patterns
        public record Member(string Name, MemberDetails Details);

        public record MemberDetails(int MonthsSubscribed, bool Blocked);

        public string[] NestedProperty(Member member)
        {
            return member switch
            {
                { Details.Blocked: true } => Array.Empty<string>(),
                { Details.MonthsSubscribed: < 3 } => new[] { "comments" },
                { Details.MonthsSubscribed: < 9 } => new[] { "comments", "mention" },
                _ => new[] { "comments", "mention", "ping" },
            };

            //early versions
            // return member switch
            // {
            //     { Details: { Blocked: true } } => Array.Empty<string>(),
            //     { Details: { MonthsSubscribed: < 3 } } => new[] { "comments" },
            //     { Details: { MonthsSubscribed: < 9 } } => new[] { "comments", "mention" },
            //     _ => new[] { "comments", "mention", "ping" },
            // };
        }


        //Type patterns
        // public string Type(object @object)
        // {
        //     return @object switch
        //     {
        //         // the variable `added` is of type `InventoryItemAdded`
        //         InventoryItemAdded added => $"Added {added.Amount}",
        //         // the variable `removed` is of type `InventoryItemRemoved`
        //         InventoryItemRemoved removed => $"Removed {removed.Amount}",
        //         InventoryItemDeactivated => "Deactivated",
        //         null => throw new ArgumentNullException(),
        //         // the variable `o` is of type `object`
        //         var o => throw new InvalidOperationException($"Unknown {o.GetType().Name}")
        //     };
        // }

        public record Appointment(DayOfWeek Day, DateTime Time, bool SocialRate);

        public int Type1(Appointment appointment)
        {
            var holidays = new List<DateTime> { };
            return appointment switch
            {
                { SocialRate: true } => 5,
                { Day: DayOfWeek.Sunday } => 25,
                Appointment a when holidays.Contains(a.Time) => 25,
                { Day: DayOfWeek.Saturday } => 20,
                { Day: DayOfWeek.Friday, Time.Hour: > 12 } => 20,
                { Time.Hour: < 8 or >= 18 } => 15,
                _ => 10,
            };
        }

        public string VariableAndTuple((string title, string name) param)
        {
            return param switch
            {
                var (title, name) when title.Equals("Mrs.") || title.Equals("Mr.") => $"Greetings {title} {name}",
                var (_, name) and (_, "Tim") => $"Hi {name}!",
                var (_, name) => $"Hello {name}",
            };
        }

        public record Person(string FirstName, string LastName, string TelephoneNumber);

        public string ConcatPerson(Person person)
        {
            return person switch
            {
                { TelephoneNumber: not null } and
                    { TelephoneNumber: not "" } info => $"{info.FirstName} {info.LastName} ({info.TelephoneNumber})",
                _ => $"{person.FirstName} {person.LastName}"
            };
        }

        //IEnumerable<string> sequence = new[] { "foo" };
        public string TuplePattern(IEnumerable<string> items)
        {
            return items switch
            {
                string[] { Length: 0 } => "array with no items",
                string[] { Length: 1 } => "array with a single item",
                string[] { Length: 2 } => "array with 2 items",
                string[] => $"array with more than 2items",
                IEnumerable<string> source when !source.Any() => "empty enumerable",
                IEnumerable<string> source when source.Count() < 3 => "a small enumerable",
                IList<string> list => $"a list with {list.Count} items",
                null => "null",
                _ => "something else"
            };
        }
    }
}