using System;
using System.Collections.Generic;

namespace ExpressionFilterApi.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;
}
