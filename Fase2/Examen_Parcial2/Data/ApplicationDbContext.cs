using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Examen_Parcial2.Models;
namespace Examen_Parcial2.Data;
public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Evento> Eventos { get; set; }
    public DbSet<Participante> Participantes { get; set; }
    public DbSet<Lugar> Lugares { get; set; }
    public DbSet<Patrocinador> Patrocinadores { get; set; }
    public DbSet<EventoParticipante> EventosParticipantes { get; set; }
    public DbSet<EventoPatrocinador> EventosPatrocinadores { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
      
        modelBuilder.Entity<EventoParticipante>()
            .HasKey(ep => new { ep.EventoId, ep.ParticipanteId });
        modelBuilder.Entity<EventoParticipante>()
            .HasOne(ep => ep.Evento)
            .WithMany(e => e.EventosParticipantes)
            .HasForeignKey(ep => ep.EventoId);
        modelBuilder.Entity<EventoParticipante>()
            .HasOne(ep => ep.Participante)
            .WithMany(p => p.EventosParticipantes)
            .HasForeignKey(ep => ep.ParticipanteId);
      
        modelBuilder.Entity<EventoPatrocinador>()
            .HasKey(ep => new { ep.EventoId, ep.PatrocinadorId });
        modelBuilder.Entity<EventoPatrocinador>()
            .HasOne(ep => ep.Evento)
            .WithMany(e => e.EventosPatrocinadores)
            .HasForeignKey(ep => ep.EventoId);
        modelBuilder.Entity<EventoPatrocinador>()
            .HasOne(ep => ep.Patrocinador)
            .WithMany(p => p.EventosPatrocinadores)
            .HasForeignKey(ep => ep.PatrocinadorId);

       
        modelBuilder.Entity<EventoPatrocinador>()
            .Property(ep => ep.MontoPatrocinio)
            .HasPrecision(18, 2); 
    }
}