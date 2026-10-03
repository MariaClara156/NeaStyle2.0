using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NeaStyle.Api.Models;

namespace NeaStyle.Api.Data;

public partial class NeaStyleContext : DbContext
{
    public NeaStyleContext()
    {
    }

    public NeaStyleContext(DbContextOptions<NeaStyleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administradore> Administradores { get; set; }

    public virtual DbSet<Carrinho> Carrinhos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Favorito> Favoritos { get; set; }

    public virtual DbSet<ItensConjunto> ItensConjuntos { get; set; }

    public virtual DbSet<ItensPedido> ItensPedidos { get; set; }

    public virtual DbSet<MovimentacoesEstoque> MovimentacoesEstoques { get; set; }

    public virtual DbSet<Pagamento> Pagamentos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Produto> Produtos { get; set; }

    public virtual DbSet<ProdutoVariaco> ProdutoVariacoes { get; set; }

    public virtual DbSet<Reembolso> Reembolsos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=NeaStyleDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administradore>(entity =>
        {
            entity.HasKey(e => e.UsuarioId);

            entity.Property(e => e.UsuarioId).HasDefaultValueSql("(NEXT VALUE FOR [UsuarioSequence])");
        });

        modelBuilder.Entity<Carrinho>(entity =>
        {
            entity.HasKey(e => e.ConjuntoProdutoId);

            entity.HasIndex(e => e.ClienteId, "IX_Carrinhos_ClienteId");

            entity.Property(e => e.ConjuntoProdutoId).HasDefaultValueSql("(NEXT VALUE FOR [ConjuntoProdutoSequence])");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Carrinhos).HasForeignKey(d => d.ClienteId);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.UsuarioId);

            entity.Property(e => e.UsuarioId).HasDefaultValueSql("(NEXT VALUE FOR [UsuarioSequence])");
            entity.Property(e => e.Cpf)
                .HasDefaultValue("")
                .HasColumnName("CPF");
        });

        modelBuilder.Entity<Favorito>(entity =>
        {
            entity.HasKey(e => e.ConjuntoProdutoId);

            entity.HasIndex(e => e.ClienteId, "IX_Favoritos_ClienteId");

            entity.Property(e => e.ConjuntoProdutoId).HasDefaultValueSql("(NEXT VALUE FOR [ConjuntoProdutoSequence])");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Favoritos).HasForeignKey(d => d.ClienteId);
        });

        modelBuilder.Entity<ItensConjunto>(entity =>
        {
            entity.HasKey(e => e.ItemConjuntoId);

            entity.ToTable("ItensConjunto");

            entity.HasIndex(e => e.CarrinhoId, "IX_ItensConjunto_CarrinhoId");

            entity.HasIndex(e => e.FavoritoId, "IX_ItensConjunto_FavoritoId");

            entity.HasIndex(e => e.ProdutoVariacaoId, "IX_ItensConjunto_ProdutoVariacaoId");

            entity.HasOne(d => d.Carrinho).WithMany(p => p.ItensConjuntos).HasForeignKey(d => d.CarrinhoId);

            entity.HasOne(d => d.Favorito).WithMany(p => p.ItensConjuntos).HasForeignKey(d => d.FavoritoId);

            entity.HasOne(d => d.ProdutoVariacao).WithMany(p => p.ItensConjuntos)
                .HasForeignKey(d => d.ProdutoVariacaoId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ItensPedido>(entity =>
        {
            entity.HasKey(e => e.ItemPedidoId);

            entity.ToTable("ItensPedido", tb => tb.HasTrigger("trg_ItensPedido_BaixarEstoque"));

            entity.HasIndex(e => e.PedidoId, "IX_ItensPedido_PedidoId");

            entity.Property(e => e.NomeProduto).HasDefaultValue("");
            entity.Property(e => e.PrecoUnitario).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Pedido).WithMany(p => p.ItensPedidos).HasForeignKey(d => d.PedidoId);
        });

        modelBuilder.Entity<MovimentacoesEstoque>(entity =>
        {
            entity.HasKey(e => e.MovimentacaoId).HasName("PK__Moviment__509C01B5F5EF2890");

            entity.ToTable("MovimentacoesEstoque");

            entity.Property(e => e.MovimentacaoId).HasColumnName("MovimentacaoID");
            entity.Property(e => e.DataMovimentacao).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.TipoMovimentacao).HasMaxLength(50);

            entity.HasOne(d => d.ProdutoVariacao).WithMany(p => p.MovimentacoesEstoques)
                .HasForeignKey(d => d.ProdutoVariacaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovimentacoesEstoque_ProdutoVariacoes");
        });

        modelBuilder.Entity<Pagamento>(entity =>
        {
            entity.HasIndex(e => e.PedidoId, "IX_Pagamentos_PedidoId");

            entity.Property(e => e.DataPagamento).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ValorPago).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Pedido).WithMany(p => p.Pagamentos).HasForeignKey(d => d.PedidoId);
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasIndex(e => e.ClienteId, "IX_Pedidos_ClienteId");

            entity.HasIndex(e => e.ClienteUsuarioId, "IX_Pedidos_ClienteUsuarioId");

            entity.HasIndex(e => e.ProdutoId, "IX_Pedidos_ProdutoId");

            entity.Property(e => e.ValorTotal).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Cliente).WithMany(p => p.PedidoClientes).HasForeignKey(d => d.ClienteId);

            entity.HasOne(d => d.ClienteUsuario).WithMany(p => p.PedidoClienteUsuarios).HasForeignKey(d => d.ClienteUsuarioId);

            entity.HasOne(d => d.Produto).WithMany(p => p.Pedidos).HasForeignKey(d => d.ProdutoId);
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.Property(e => e.Ativo).HasDefaultValue(true);
            entity.Property(e => e.Descricao).HasDefaultValue("");
        });

        modelBuilder.Entity<ProdutoVariaco>(entity =>
        {
            entity.HasKey(e => e.ProdutoVariacaoId);

            entity.ToTable(tb => tb.HasTrigger("trg_ProdutoVariacoes_AuditarEstoque"));

            entity.HasIndex(e => e.ProdutoId, "IX_ProdutoVariacoes_ProdutoId");

            entity.Property(e => e.EstoqueMinimo).HasDefaultValue(5);
            entity.Property(e => e.Preco).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PrecoCusto).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Produto).WithMany(p => p.ProdutoVariacos).HasForeignKey(d => d.ProdutoId);
        });

        modelBuilder.Entity<Reembolso>(entity =>
        {
            entity.HasIndex(e => e.PedidoId, "IX_Reembolsos_PedidoId");

            entity.Property(e => e.ValorReembolso).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Pedido).WithMany(p => p.Reembolsos).HasForeignKey(d => d.PedidoId);
        });
        modelBuilder.HasSequence("ConjuntoProdutoSequence");
        modelBuilder.HasSequence("UsuarioSequence");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
