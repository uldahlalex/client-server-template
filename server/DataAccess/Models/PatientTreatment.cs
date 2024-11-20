﻿namespace DataAccess.Models;

public class PatientTreatment
{
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }

    public Guid TreatmentId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual Treatment Treatment { get; set; } = null!;
}