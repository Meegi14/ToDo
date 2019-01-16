using System;

namespace ToDo.Models
{
    internal class DefaulValueAttribute : Attribute
    {
        private bool v;

        public DefaulValueAttribute(bool v)
        {
            this.v = v;
        }
    }
}