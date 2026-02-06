using System;
using System.Collections.Generic;
using Checkout.Common;
using Checkout.Identities.Common;

namespace Checkout.Identities.AmlScreening.Responses
{
    public class AmlScreeningResponse : Resource
    {
        public string Id { get; set; }

        public string ApplicantId { get; set; }

        public string Reference { get; set; }

        public AmlScreeningStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public SearchParameters SearchParameters { get; set; }

        public List<AmlMatch> Matches { get; set; }

        public string Reason { get; set; }
    }

    public class AmlMatch
    {
        public string Id { get; set; }

        public string Source { get; set; }

        public string MatchType { get; set; }

        public double MatchScore { get; set; }

        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public List<string> Aliases { get; set; }

        public List<string> Categories { get; set; }

        public string Description { get; set; }

        public List<string> Countries { get; set; }

        public bool IsPep { get; set; }

        public bool IsSanction { get; set; }

        public bool IsAdverseMedia { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}