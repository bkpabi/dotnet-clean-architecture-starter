using CleanArch.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Entities
{
    public class Category : BaseEntity
    {
        public Category() { }

        public Category(string name
            , string description
            , string userId
            , bool isActive
            , string createdBy
            , DateTime createdOn
            , string updatedBy
            , DateTime updatedOn)
        {
            CategoryName = name;
            Description = description;
            UserId = userId;
            IsActive = isActive;
            CreatedBy = createdBy;
            CreatedOn = createdOn;
            UpdatedBy = updatedBy;
            UpdatedOn = updatedOn;
        }

        public string CategoryName { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string? UserId { get; private set; }
        public bool IsActive { get; private set; } = true;

    }
}
