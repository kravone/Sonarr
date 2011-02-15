﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SubSonic.SqlGeneration.Schema;

namespace NzbDrone.Core.Repository.Quality
{
    public class QualityProfile
    {
        [SubSonicPrimaryKey(true)]
        public int ProfileId { get; set; }

        [Required (ErrorMessage = "A Name is Required")]
        [DisplayName("Name")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get; set; }
        public bool UserProfile { get; set; } //Allows us to tell the difference between default and user profiles

        [SubSonicIgnore]
        [DisplayName("Allowed Qualities")]
        public List<QualityTypes> Allowed { get; set; }

        [SubSonicIgnore]
        [DisplayName("Allowed Qualities String")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string AllowedString { get; set; }

        [DisplayName("Cutoff")]
        [Required(ErrorMessage = "Valid Cutoff is Required")]
        public QualityTypes Cutoff { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string SonicAllowed
        {
            get
            {
                string result = String.Empty;
                foreach (var q in Allowed)
                {
                    result += (int)q + "|";
                }
                return result.Trim('|');
            }
            private set
            {
                var qualities = value.Split('|');
                Allowed = new List<QualityTypes>(qualities.Length);
                foreach (var quality in qualities)
                {
                    Allowed.Add((QualityTypes)Convert.ToInt32(quality));
                }
            }
        }
    }
}
