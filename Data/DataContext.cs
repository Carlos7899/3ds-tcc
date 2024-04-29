using BairroConnectAPI.Models;
using BairroConnectAPI.Models.Enuns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using BairroConnectAPI.Utils;
using Microsoft.Extensions.Options;

namespace BairroConnectAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            
        }

        #region Tabelas
        public DbSet<Logins> TB_LOGINS { get; set; }
        public DbSet<Evento> TB_EVENTO { get; set; }
        public DbSet<Equipe> TB_EQUIPE { get; set; }
        public DbSet<Categoria> TB_CATEGORIA { get; set; }
        public DbSet<Municipe> TB_MUNICIPE { get; set; }
        public DbSet<EventoEndereco> TB_EVENTOENDERECO { get; set; }
        public DbSet<EventoParticipante> TB_EVENTOPARTICIPANTE { get; set; }
        public DbSet<EventoMunicipe> TB_EVENTOMUNICIPE { get; set; }
        public DbSet<EventoComentario> TB_EVENTOCOMENTARIO { get; set; }
        public DbSet<OrgEventos> TB_ORGEVENTOS { get; set; }


        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Logins>()

            #region Logins

                .ToTable("TB_LOGINS");

            modelBuilder.Entity<Logins>()
                .HasKey(l => l.idPessoa);

            modelBuilder.Entity<Logins>()
                .Property(l => l.idPessoa)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Logins>()
                .Property(l => l.nome)
                .HasMaxLength(50);

            modelBuilder.Entity<Logins>()
                 .Property(l => l.sobrenome)
                 .HasMaxLength(50);

            modelBuilder.Entity<Logins>()
                .Property(l => l.email)
                .HasMaxLength(100);

            modelBuilder.Entity<Logins>()
                .Property(l => l.dataNasc)
                .HasColumnType("date");

            modelBuilder.Entity<Logins>()
                .Property(l => l.tipoConta)
                .HasConversion(new EnumToStringConverter<TipoConta>());


            modelBuilder.Entity<Logins>()
                .Ignore(u => u.senha);

            modelBuilder.Entity<Logins>()
                .Property(u => u.PasswordHash)
                .HasMaxLength(255);

            modelBuilder.Entity<Logins>()
                .Property(u => u.PasswordSalt)
                .HasMaxLength(255);

            modelBuilder.Entity<Logins>()
                .Ignore(l => l.Token);

            Logins user = new Logins();
            {
                user.idPessoa = 1;
                user.nome = "UsuarioAdmin";
                user.sobrenome = "";
                user.senha = string.Empty;
                user.email = "seuEmail@gmail.com";
                user.dataNasc = DateTime.Now;
                user.tipoConta = TipoConta.Organizador;
                Criptografia.CriarPasswordHash("123456", out byte[] hash, out byte[] salt);
                user.Foto = null;
                user.Token = string.Empty;
                user.PasswordHash = hash;
                user.PasswordSalt = salt;

            };

            modelBuilder.Entity<Logins>().HasData(user);
            modelBuilder.Entity<Logins>().Property(u => u.tipoConta).HasDefaultValue(TipoConta.Municipe);

            #endregion

            #region OrgEventos

            modelBuilder.Entity<OrgEventos>()
                 .ToTable("TB_ORGEVENTOS"); // Define o nome da tabela

            modelBuilder.Entity<OrgEventos>()
                .HasKey(o => o.idOrganizador); // Define a chave primária

            modelBuilder.Entity<OrgEventos>()
                .Property(o => o.idOrganizador)
                .ValueGeneratedOnAdd(); // Define a geração automática do valor da chave primária

            modelBuilder.Entity<OrgEventos>()
                .Property(o => o.profissao)
                .HasMaxLength(100); // Define o tamanho máximo para a profissão

            modelBuilder.Entity<OrgEventos>()
                .Property(o => o.empresa)
                .HasMaxLength(100); // Define o tamanho máximo para a empresa

            modelBuilder.Entity<OrgEventos>()
                .Property(o => o.telOrganizador)
                .HasMaxLength(20); // Define o tamanho máximo para o telefone do organizador

            modelBuilder.Entity<OrgEventos>()
                     .HasOne(o => o.logins) // Define a relação de chave estrangeira
                     .WithOne() // Define que um organizador tem apenas um login
                     .HasForeignKey<OrgEventos>(o => o.idPessoa); // Define a chave estrangeira

            #endregion

            #region Municipe

            modelBuilder.Entity<Municipe>()
                 .ToTable("TB_MUNICIPE"); // Define o nome da tabela

            modelBuilder.Entity<Municipe>()
                .HasKey(m => m.idMunicipe); // Define a chave primária

            modelBuilder.Entity<Municipe>()
                .Property(m => m.idMunicipe)
                .ValueGeneratedOnAdd(); // Define a geração automática do valor da chave primária

            modelBuilder.Entity<Municipe>()
                .Property(m => m.estado)
                .HasMaxLength(100); // Define o tamanho máximo para o estado

            modelBuilder.Entity<Municipe>()
                .Property(m => m.cidade)
                .HasMaxLength(100); // Define o tamanho máximo para a cidade

            modelBuilder.Entity<Municipe>()
                .HasOne(m => m.logins) // Define a relação de chave estrangeira
                .WithOne() // Define que um munícipe tem apenas um login
                .HasForeignKey<Municipe>(m => m.idPessoa); // Define a chave estrangeira

            #endregion

            #region Categoria

            modelBuilder.Entity<Categoria>()
                .ToTable("TB_CATEGORIA"); // Define o nome da tabela

            modelBuilder.Entity<Categoria>()
                .HasKey(c => c.idCategoria); // Define a chave primária

            modelBuilder.Entity<Categoria>()
                .Property(c => c.idCategoria)
                .ValueGeneratedOnAdd(); // Define a geração automática do valor da chave primária

            modelBuilder.Entity<Categoria>()
                .Property(c => c.nomeCategoria)
                .HasMaxLength(100); // Define o tamanho máximo para o nome da categoria

            modelBuilder.Entity<Categoria>()
                .Property(c => c.descricao)
                .HasMaxLength(255); // Define o tamanho máximo para a descrição


            modelBuilder.Entity<Categoria>().HasData
            (
                   new Categoria() { idCategoria = 1, nomeCategoria = "Esportivo", descricao = "Atividades físicas e competições recreativas." },
                   new Categoria() { idCategoria = 2, nomeCategoria = "Entreterimento", descricao = "Diversão e lazer para todos os gostos." },
                   new Categoria() { idCategoria = 3, nomeCategoria = "Cultaral", descricao = "Exploração da arte, história e tradições." },
                   new Categoria() { idCategoria = 4, nomeCategoria = "Corporativo", descricao = "Eventos voltados para negócios." },
                   new Categoria() { idCategoria = 5, nomeCategoria = "Religioso", descricao = "Práticas e celebrações voltadas para a religião." },
                   new Categoria() { idCategoria = 7, nomeCategoria = "Educacional", descricao = "Eventos voltados para educação." },
                   new Categoria() { idCategoria = 8, nomeCategoria = "Institucional", descricao = "Eventos relacionados a organizações e instituições." }

            );


            #endregion

            #region Evento

            modelBuilder.Entity<Evento>()
                 .ToTable("TB_EVENTO"); // Define o nome da tabela

            modelBuilder.Entity<Evento>()
                .HasKey(e => e.idEvento); // Define a chave primária

            modelBuilder.Entity<Evento>()
                .Property(e => e.idEvento)
                .ValueGeneratedOnAdd(); // Define a geração automática do valor da chave primária

            modelBuilder.Entity<Evento>()
                .HasOne<OrgEventos>() // Define a relação de chave estrangeira com a classe Evento
                .WithOne() // Define que uma equipe pertence a um evento
                .HasForeignKey<Evento>(e => e.idOrganizador) // Define a chave estrangeira
                .IsRequired(); // Define que a chave estrangeira é obrigatória


            modelBuilder.Entity<Evento>()
                .HasOne<Categoria>() // Define a relação de chave estrangeira com a classe Evento
                .WithOne() // Define que uma equipe pertence a um evento
                .HasForeignKey<Evento>(e => e.idCategoria) // Define a chave estrangeira
                .IsRequired(); // Define que a chave estrangeira é obrigatória

            modelBuilder.Entity<Evento>()
               .Property(e => e.titulo)
               .IsRequired();

            modelBuilder.Entity<Evento>()
                .Property(e => e.dataInicio)
                .HasColumnType("datetime2")
                .IsRequired();

            modelBuilder.Entity<Evento>()
                .Property(e => e.dataFim)
                .HasColumnType("datetime2")
                .IsRequired();

            modelBuilder.Entity<Evento>()
                .Property(e => e.limiteParticipantes)
                .IsRequired();

            modelBuilder.Entity<Evento>()
                .Property(e => e.descricao)
                .IsRequired();

            modelBuilder.Entity<Evento>()
                .Property(e => e.valorIngresso)
                .IsRequired();

            modelBuilder.Entity<Evento>()
                .Property(e => e.horaInicio)
                .HasColumnType("datetime2")
                .IsRequired();

            modelBuilder.Entity<Evento>()
                .Property(e => e.horaFim)
                .HasColumnType("datetime2")
                .IsRequired();


            #endregion

            #region Equipe


            modelBuilder.Entity<Equipe>()
                .ToTable("TB_EQUIPE"); // Define o nome da tabela

            modelBuilder.Entity<Equipe>()
                .HasNoKey();

            modelBuilder.Entity<Equipe>()
                .Property(e => e.respoEquipe)
                .HasMaxLength(100); // Define o tamanho máximo para o responsável pela equipe

            modelBuilder.Entity<Equipe>()
               .Property(e => e.tamanhoEquipe); // Configura a propriedade tamanhoEquipe


            modelBuilder.Entity<Equipe>()
                .HasOne<Evento>() // Define a relação de chave estrangeira com a classe Evento
                .WithOne() // Define que uma equipe pertence a um evento
                .HasForeignKey<Equipe>(e => e.idEvento) // Define a chave estrangeira
                .IsRequired(); // Define que a chave estrangeira é obrigatória


            #endregion

            #region EventoComentario

            modelBuilder.Entity<EventoComentario>()
             .ToTable("TB_EVENTOCOMENTARIO"); // Define o nome da tabela
           
            modelBuilder.Entity<EventoComentario>()
               .HasNoKey();

            modelBuilder.Entity<EventoComentario>()
                .Property(ec => ec.comentario)
                .HasMaxLength(255); // Define o tamanho máximo para o comentário

            modelBuilder.Entity<EventoComentario>()
                .Property(ec => ec.avaliacao)
                .HasColumnType("float"); // Define o tipo de dado para a avaliação


            modelBuilder.Entity<EventoComentario>()
                 .HasOne<Evento>() // Define a relação de chave estrangeira com a classe Evento
                 .WithOne() // Define que uma equipe pertence a um evento
                 .HasForeignKey<EventoComentario>(e => e.idEvento);

            #endregion

            #region EventoEndereco

            modelBuilder.Entity<EventoEndereco>()
                .ToTable("TB_EVENTOENDERECO"); // Define o nome da tabela

            modelBuilder.Entity<EventoEndereco>()
                .HasNoKey();

            modelBuilder.Entity<EventoEndereco>()
                .Property(ee => ee.endereco)
                .HasMaxLength(255); // Define o tamanho máximo para o endereço

            modelBuilder.Entity<EventoEndereco>()
                .Property(ee => ee.nroEndereco)
                .HasMaxLength(20); // Define o tamanho máximo para o número do endereço

            modelBuilder.Entity<EventoEndereco>()
                .Property(ee => ee.Complemento)
                .HasMaxLength(100); // Define o tamanho máximo para o complemento (opcional)

            modelBuilder.Entity<EventoEndereco>()
                .Property(ee => ee.bairroEndereco)
                .HasMaxLength(100); // Define o tamanho máximo para o bairro

            modelBuilder.Entity<EventoEndereco>()
                .Property(ee => ee.cidadeEndereco)
                .HasMaxLength(100); // Define o tamanho máximo para a cidade

            modelBuilder.Entity<EventoEndereco>()
                .Property(ee => ee.UFEndereco)
                .HasMaxLength(2); // Define o tamanho máximo para a UF (Unidade Federativa)

            modelBuilder.Entity<EventoEndereco>()
                .Property(ee => ee.CEPEndereco)
                .HasMaxLength(8); // Define o tamanho máximo para o C

            modelBuilder.Entity<EventoEndereco>()
                 .HasOne<Evento>() // Define a relação de chave estrangeira com a classe Evento
                 .WithOne() // Define que uma equipe pertence a um evento
                 .HasForeignKey<EventoEndereco>(e => e.idEvento); // Define a chave estrangeira
            

            #endregion

            #region EventoMunicipe

            modelBuilder.Entity<EventoMunicipe>()
               .ToTable("TB_EVENTOMUNICIPE"); // Define o nome da tabela

            modelBuilder.Entity<EventoMunicipe>()
                .HasKey(em => em.idEventoMunicipe); // Define a chave primária

            modelBuilder.Entity<EventoMunicipe>()
                .Property(em => em.horaInicio)
                .HasColumnType("datetime"); // Define o tipo de dado para a horaInicio

            modelBuilder.Entity<EventoMunicipe>()
                .Property(em => em.horaFim)
                .HasColumnType("datetime"); // Define o tipo 

          
            #endregion

            #region EventoParticipante

            modelBuilder.Entity<EventoParticipante>()
               .ToTable("TB_EVENTOPARTICIPANTE"); // Define o nome da tabela

            modelBuilder.Entity<EventoParticipante>()
                .HasNoKey();

            modelBuilder.Entity<EventoParticipante>()
                .Property(ep => ep.horaParticipacao)
                .HasColumnType("datetime");

            modelBuilder.Entity<EventoParticipante>()
               .HasOne<Evento>() // Define a relação de chave estrangeira com a classe Evento
               .WithOne() // Define que uma equipe pertence a um evento
               .HasForeignKey<EventoParticipante>(e => e.idEvento); // Define a chave estrangeira
            


            #endregion



        }
    }
}
