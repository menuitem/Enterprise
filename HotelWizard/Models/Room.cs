using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelWizard.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        public int roomNum { get; set; }
        public int roomRate { get; set; }
        public String roomType { get; set; }

        public static SelectList getAllRooms()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            //sql query to get all rooms from the database
            string query = "SELECT * FROM Rooms";
            System.Data.Entity.Infrastructure.DbRawSqlQuery<Room> data = db.Rooms.SqlQuery(query);
            
            //put room id's only into a new list
            List<int> mylist = new List<int>();

            foreach (Room e in data.ToList())
            {
                mylist.Add(e.RoomId);
            }

            return new SelectList(mylist);
        
        }

        public static SelectList getFreeRooms(DateTime start, DateTime enddate){
        
            //put search by dates into an onject parameter, for passing to the sql query
            Object[] dates = new Object[2];
            dates[0] = start;
            dates[1] = enddate;

            ApplicationDbContext db = new ApplicationDbContext();
            //search for available rooms for the given dates
            string query = "SELECT * FROM Rooms r where not exists(SELECT RoomId from RoomBookings rb where rb.RoomId=r.RoomId and ((@p0 between rb.checkin and rb.checkout) or (@p1 between rb.checkin and rb.checkout)))";           
            System.Data.Entity.Infrastructure.DbRawSqlQuery<Room> data = db.Rooms.SqlQuery(query, dates);

            //add only the room id's to a new list
            List<int> mylist = new List<int>();
 
            foreach (Room e in data.ToList())
            {
                mylist.Add(e.RoomId);
            }

            return new SelectList(mylist);

        }
    }
}