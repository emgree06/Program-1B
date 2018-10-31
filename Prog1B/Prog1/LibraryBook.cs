// Program 1B
// CIS 200-01
// Due: 2/21/2018
// By: Z9435

// File: LibraryBook.cs
// This file creates a concrete LibraryBook class that adds
// an author to the LibraryItem data. 
// LibraryBook IS-A LibraryItem

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryItems
{
    public class LibraryBook : LibraryItem
    {
        public const decimal DAILYLATEFEE = 0.25m; // Book's daily late fee

        private string _author; // The book's author

        // Precondition:  theCopyrightYear >= 0, theLoanPeriod >= 0
        //                theTitle and theCallNumber must not be null or empty
        // Postcondition: The library book has been initialized with the specified
        //                values for title, publisher, copyright year, loan period, 
        //                call number, and author. The item is not checked out.
        public LibraryBook(string theTitle, string thePublisher, int theCopyrightYear,
            int theLoanPeriod, string theCallNumber, string theAuthor) :
            base(theTitle, thePublisher, theCopyrightYear, theLoanPeriod, theCallNumber)
        {
            Author = theAuthor;
        }

        public string Author
        {
            // Precondition:  None
            // Postcondition: The author has been returned
            get
            {
                return _author;
            }

            // Precondition:  None
            // Postcondition: The author has been set to the specified value
            set
            {
                // Since empty author is OK, just change null to empty string
                _author = (value == null ? string.Empty : value.Trim());
            }
        }

        // Precondition:  daysLate >= 0
        // Postcondition: The fee for returning the item the specified days late
        //                has been returned
        public override decimal CalcLateFee(int daysLate)
        {
            ValidateDaysLate(daysLate);

            return daysLate * DAILYLATEFEE;
        }

        // Precondition:  None
        // Postcondition: A string is returned presenting the libary item's data on
        //                separate lines
        public override string ToString()
        {
            string NL = Environment.NewLine; // NewLine shortcut

            return $"LibraryBook{NL}Author: {Author}{NL}{base.ToString()}";
        }
    }
}
