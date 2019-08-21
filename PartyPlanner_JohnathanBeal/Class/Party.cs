using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyPlanner_JohnathanBeal.Class
{
    class Party
    {
        private double costPerCapita;
        private string[] guestList;
        private int _maxNumOfGuests;

        public double CostPerCapita { get { return this.costPerCapita; } set { costPerCapita = value; } }

        public Party(int maxNumOfGuests)
        {
            _maxNumOfGuests = maxNumOfGuests;
            guestList = new string[_maxNumOfGuests];
        }

        public int NumberOfGuests()
        {
            int actualInvitees = 0;
            foreach (var guest in guestList)
            {
                if (!string.IsNullOrEmpty(guest))
                {
                    actualInvitees++;
                }
            }
            return actualInvitees;
        }

        public string AddName(string firstofhisnames, string lastname)
        {
            return lastname.ToUpper() + ", " + firstofhisnames;
        }

        public Tuple<string, string> SplitName(string name)
        {
            var firstAndLastName = name.Split(',');
            return new Tuple<string, string>(firstAndLastName[0], firstAndLastName[1]);      
        }


        public bool AddToGuestList(string firstofhisnames, string lastname, out string message)
        {
            bool guestWasAddedToList = false;
            message = "";
            if(guestList.All(ne => !string.IsNullOrEmpty(ne)))
            {
                message = "The maximum number of invitees has been reached";
            }
            else
            {
                for (int i = 0; i <= guestList.Length - 1; i++)
                {
                    var guest = guestList[i];
                    if (string.IsNullOrEmpty(guest))
                    {
                        guestList[i] = AddName(firstofhisnames, lastname);
                        message = "a new invitee has been added to the guestlist";
                        guestWasAddedToList = true;
                        break;
                    }
                }
            }
            return guestWasAddedToList;
        }

        public string UpdateGuestList(string firstofhisnames, string lastname, int index)
        {              
            guestList[index] = AddName(firstofhisnames, lastname);
            return guestList[index];
        }

        public string[] GetGuestList()
        {
            return guestList;
        }

        public string GetLastGuestAdded()
        {
            for (int i = guestList.Length - 1; i >= 0; i--)
            {
                var guest = guestList[i];

                if (!string.IsNullOrEmpty(guest))
                {
                    return guest;
                }
            }
            return "";
        }

        internal bool RemoveGuests(int removeIndex)
        {

            var before = guestList[removeIndex];

            guestList[removeIndex] = "";

            for (int i = removeIndex; i < guestList.Length - 1; i++)
            {
                    guestList[i] = guestList[i + 1];           
            }
            guestList[guestList.Length - 1] = "";

            var after = guestList[removeIndex];
            try
            {
                Assert.AreNotSame(before, after);
                return true;
            }
            catch
            {
                return false;
            }
            
        }
    }
}
