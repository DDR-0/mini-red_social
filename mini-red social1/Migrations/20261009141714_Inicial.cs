using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mini_red_social1.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recursos",
                columns: table => new
                {
                    recurso_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    capacidad = table.Column<int>(type: "int", nullable: false),
                    creado_en = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modificado_en = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recursos", x => x.recurso_id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    roles_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: false),
                    creado_en = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modificado_en = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.roles_id);
                });

            migrationBuilder.CreateTable(
                name: "SnapshotsDiarios",
                columns: table => new
                {
                    snapshot_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarios_activos_totales = table.Column<int>(type: "int", nullable: false),
                    interacciones_totales_creadas = table.Column<int>(type: "int", nullable: false),
                    interacciones_totales_resueltas = table.Column<int>(type: "int", nullable: false),
                    frecuencia_uso_recursos_usados = table.Column<decimal>(type: "decimal(9,6)", precision: 9, scale: 6, nullable: false),
                    metadatos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    creado_en = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnapshotsDiarios", x => x.snapshot_id);
                });

            migrationBuilder.CreateTable(
                name: "TiposInteraccion",
                columns: table => new
                {
                    tipo_interaccion_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    requiere_recurso = table.Column<bool>(type: "bit", nullable: false),
                    esta_activo = table.Column<bool>(type: "bit", nullable: false),
                    creado_en = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modificado_en = table.Column<DateTime>(type: "datetime2", nullable: false),
                    borrado_en = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposInteraccion", x => x.tipo_interaccion_id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rol_id = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    correo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contrasena_hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    estado_usuario = table.Column<int>(type: "int", nullable: false),
                    visiblity = table.Column<int>(type: "int", nullable: false),
                    creado_en = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modificado_en = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estado_cuenta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_rol_id",
                        column: x => x.rol_id,
                        principalTable: "Roles",
                        principalColumn: "roles_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interacciones",
                columns: table => new
                {
                    interacciones_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipo_interaccion_id = table.Column<int>(type: "int", nullable: false),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    titulo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    EstadoActual = table.Column<int>(type: "int", nullable: false),
                    NivelPrivacidad = table.Column<int>(type: "int", nullable: false),
                    horario_inicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    horario_fin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    creado_en = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modificado_en = table.Column<DateTime>(type: "datetime2", nullable: false),
                    borrado_en = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interacciones", x => x.interacciones_id);
                    table.ForeignKey(
                        name: "FK_Interacciones_TiposInteraccion_tipo_interaccion_id",
                        column: x => x.tipo_interaccion_id,
                        principalTable: "TiposInteraccion",
                        principalColumn: "tipo_interaccion_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interacciones_Usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistrosAuditoria",
                columns: table => new
                {
                    auditlog_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    accion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    entidad_afectada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_entidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    direccion_ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    usuario_agente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    payload_antes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    payload_despues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    creado_en = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosAuditoria", x => x.auditlog_id);
                    table.ForeignKey(
                        name: "FK_RegistrosAuditoria_Usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistorialInteracciones",
                columns: table => new
                {
                    historial_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    interaccion_id = table.Column<int>(type: "int", nullable: false),
                    cambio_por_usuario = table.Column<int>(type: "int", nullable: false),
                    estado_previo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    estado_nuevo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    motivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    creado_en = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialInteracciones", x => x.historial_id);
                    table.ForeignKey(
                        name: "FK_HistorialInteracciones_Interacciones_interaccion_id",
                        column: x => x.interaccion_id,
                        principalTable: "Interacciones",
                        principalColumn: "interacciones_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistorialInteracciones_Usuarios_cambio_por_usuario",
                        column: x => x.cambio_por_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "UsosRecurso",
                columns: table => new
                {
                    uso_recurso_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    interaccion_id = table.Column<int>(type: "int", nullable: false),
                    recurso_id = table.Column<int>(type: "int", nullable: false),
                    fecha_inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fecha_fin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado_recurso = table.Column<int>(type: "int", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fecha_eliminacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsosRecurso", x => x.uso_recurso_id);
                    table.ForeignKey(
                        name: "FK_UsosRecurso_Interacciones_interaccion_id",
                        column: x => x.interaccion_id,
                        principalTable: "Interacciones",
                        principalColumn: "interacciones_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsosRecurso_Recursos_recurso_id",
                        column: x => x.recurso_id,
                        principalTable: "Recursos",
                        principalColumn: "recurso_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistorialInteracciones_cambio_por_usuario",
                table: "HistorialInteracciones",
                column: "cambio_por_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialInteracciones_interaccion_id",
                table: "HistorialInteracciones",
                column: "interaccion_id");

            migrationBuilder.CreateIndex(
                name: "IX_Interacciones_id_usuario",
                table: "Interacciones",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Interacciones_tipo_interaccion_id",
                table: "Interacciones",
                column: "tipo_interaccion_id");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosAuditoria_id_usuario",
                table: "RegistrosAuditoria",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_SnapshotsDiarios_fecha",
                table: "SnapshotsDiarios",
                column: "fecha",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsosRecurso_interaccion_id",
                table: "UsosRecurso",
                column: "interaccion_id");

            migrationBuilder.CreateIndex(
                name: "IX_UsosRecurso_recurso_id",
                table: "UsosRecurso",
                column: "recurso_id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_correo",
                table: "Usuarios",
                column: "correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_rol_id",
                table: "Usuarios",
                column: "rol_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistorialInteracciones");

            migrationBuilder.DropTable(
                name: "RegistrosAuditoria");

            migrationBuilder.DropTable(
                name: "SnapshotsDiarios");

            migrationBuilder.DropTable(
                name: "UsosRecurso");

            migrationBuilder.DropTable(
                name: "Interacciones");

            migrationBuilder.DropTable(
                name: "Recursos");

            migrationBuilder.DropTable(
                name: "TiposInteraccion");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
