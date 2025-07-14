﻿namespace Mouts.SalesDeveloper.Domain.Common
{

    public class BaseEntity : IComparable<BaseEntity>
    {
        public Guid Id { get; set; }

        public int CompareTo(BaseEntity? other)
        {
            if (other == null)
            {
                return 1;
            }

            return other!.Id.CompareTo(Id);
        }
    }
}
