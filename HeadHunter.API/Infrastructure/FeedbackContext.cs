﻿using HeadHunter.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HeadHunter.API.Infrastructure
{
    public class FeedbackContext : DbContext
    {
        public FeedbackContext(DbContextOptions<FeedbackContext> options) : base(options)
        {
        }

        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Identity> Identities { get; set; }
    }
}