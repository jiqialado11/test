﻿using System;
using System.Collections.Generic;

namespace Individuals.DataAccess.Models.QueryIndividuals
{
    public class QueryIndividualsRequest
    {
        public string QueryString { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PersonalId { get; set; }
        public int? Gender { get; set; }
        public string City { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public string OrderBy { get; set; }
    }

    public class QueryIndividualsResponse
    {
        public List<QueryIndividualsItem> List { get; set; }
    }

}
