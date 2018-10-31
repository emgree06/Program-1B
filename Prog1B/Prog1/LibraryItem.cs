// Program 1B
// CIS 200-01
// Due: 2/21/2018
// By: Z9435

// File: LibraryItem.cs
// This file creates an abstract LibraryItem class that will serve
// as the base class of a hierarchy of library items that keep
// track of common information and can be checked out by
// LibraryPatrons.
// LibraryItem HAS-A LibraryPatron (when the item is checked out)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryItems
{
    public abstract class LibraryItem
    {
        private string _title;      // The book's title
        private string _author;     // The book's author
        private string _publisher;  // The book's publisher
        private int _copyrightYear; // The book's year of copyright
        private string _callNumber; // The book's call number in the library
        private bool _checkedOut;   // The book's checked out status
        private int _loanPeriod;    // The item's loan period

        // Precondition:  theCopyrightYear >= 0, theLoanPeriod >= 0
        //                theTitle and theCallNumber must not be null or empty
        // Postcondition: The library item has been initialized with the specified
        //                values for title, publisher, copyright year, loan period and
        //                call number. The item is not checked out.
        public LibraryItem(string theTitle, string thePublisher, int theCopyrightYear,
            int theLoanPeriod, string theCallNumber)
        {
            Title = theTitle;
            Publisher = thePublisher;
            CopyrightYear = theCopyrightYear;
            LoanPeriod = theLoanPeriod;
            CallNumber = theCallNumber;

            ReturnToShelf(); // Make sure item is not checked out
        }

        public string Title
        {
            // Precondition:  None
            // Postcondition: The title has been returned
            get
            {
                return _title;
            }

            // Precondition:  value must not be null or empty
            // Postcondition: The title has been set to the specified value
            set
            {
                if (string.IsNullOrWhiteSpace(value)) // IsNullOrWhiteSpace includes tests for null, empty, or all whitespace
                    throw new ArgumentOutOfRangeException($"{nameof(Title)}", value,
                        $"{nameof(Title)} must not be null or empty");
                else
                    _title = value.Trim();
            }
        }

        public string Publisher
        {
            // Precondition:  None
            // Postcondition: The publisher has been returned
            get
            {
                return _publisher;
            }

            // Precondition:  None
            // Postcondition: The publisher has been set to the specified value
            set
            {
                // Since empty publisher is OK, just change null to empty string
                _publisher = (value == null ? string.Empty : value.Trim());
            }
        }

        public int CopyrightYear
        {
            // Precondition:  None
            // Postcondition: The copyright year has been returned
            get
            {
                return _copyrightYear;
            }

            // Precondition:  value >= 0
            // Postcondition: The copyright year has been set to the specified value
            set
            {
                if (value >= 0)
                    _copyrightYear = value;
                else
                    throw new ArgumentOutOfRangeException($"{nameof(CopyrightYear)}", value,
                        $"{nameof(CopyrightYear)} must be >= 0");
            }
        }

        public string CallNumber
        {
            // Precondition:  None
            // Postcondition: The call number has been returned
            get
            {
                return _callNumber;
            }

            // Precondition:  value must not be null or empty
            // Postcondition: The call number has been set to the specified value
            set
            {
                if (string.IsNullOrWhiteSpace(value)) // IsNullOrWhiteSpace includes tests for null, empty, or all whitespace
                    throw new ArgumentOutOfRangeException($"{nameof(CallNumber)}", value,
                        $"{nameof(CallNumber)} must not be null or empty");
                else
                    _callNumber = value.Trim();
            }
        }

        public int LoanPeriod
        {
            // Precondition:  None
            // Postcondition: The loan period has been returned
            get
            {
                return _loanPeriod;
            }

            // Precondition:  value >= 0
            // Postcondition: The loan period has been set to the specified value
            set
            {
                if (value >= 0)
                    _loanPeriod = value;
                else
                    throw new ArgumentOutOfRangeException($"{nameof(LoanPeriod)}", value,
                        $"{nameof(LoanPeriod)} must be >= 0");
            }
        }
        // Create HAS-A
        public LibraryPatron Patron
        {
            // Precondition:  None
            // Postcondition: The book's patron has been returned
            get; // Auto-implement is fine

            // Helper
            // Precondition:  None
            // Postcondition: The book's patron has been set to the specified value
            private set; // Auto-implement is fine 
        }

        // Abstract method header
        // Precondition:  daysLate >= 0
        // Postcondition: The fee for returning the item the specified days late
        //                has been returned
        public abstract decimal CalcLateFee(int daysLate);

        // Precondition:  thePatron != null
        // Postcondition: The book is checked out
        public void CheckOut(LibraryPatron thePatron)
        {
            if (thePatron != null)
                Patron = thePatron;
            else
                throw new ArgumentNullException($"{nameof(thePatron)}", $"{nameof(thePatron)} must not be null");

            _checkedOut = true;
        }

        // Precondition:  None
        // Postcondition: The book is not checked out
        public void ReturnToShelf()
        {
            Patron = null; // Remove previously stored reference to patron

            _checkedOut = false;
        }

        // Precondition:  None
        // Postcondition: true is returned if the book is checked out,
        //                otherwise false is returned
        public bool IsCheckedOut()
        {
            return _checkedOut;
        }

        // Precondition:  None
        // Postcondition: A string is returned presenting the libary item's data on
        //                separate lines
        public override string ToString()
        {
            string NL = Environment.NewLine; // NewLine shortcut
            string checkedOutBy; // Holds checked out message

            if (IsCheckedOut())
                checkedOutBy = $"Checked Out By: {NL}{Patron}";
            else
                checkedOutBy = "Not Checked Out";

            return $"Title: {Title}{NL}Publisher: {Publisher}{NL}" +
                $"Copyright: {CopyrightYear}{NL}Loan Period: {LoanPeriod}{NL}" +
                $"Call Number: {CallNumber}{NL}{checkedOutBy}";
        }

        // HELPER METHOD - for derived classes
        // Precondition:  daysLate >= 0
        // Postcondition: If daysLate invalid, throw exception
        protected void ValidateDaysLate(int daysLate)
        {
            if (daysLate < 0)
                throw new ArgumentOutOfRangeException($"{nameof(daysLate)}", daysLate,
                    $"{nameof(daysLate)} must be >= 0");
        }
    }
}
