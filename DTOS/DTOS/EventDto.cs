using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLiberary.DTOS
{
    public  class EventDto
    {
      
            public string Name { get; set; }           
            public string Description { get; set; }
            public string ImageUrl { get; set; }       
            public int Category { get; set; }         
            public decimal Price { get; set; }         
            public DateTime Date { get; set; }        
            public string Venue { get; set; }        
        }

    
}
