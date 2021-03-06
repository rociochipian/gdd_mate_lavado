CREATE PROCEDURE getPuntos_sp
@username varchar(30),
@fecha DATETIME
AS
BEGIN
	SELECT COALESCE(SUM(cantidad_puntos), 0) 'cantidad_puntos' FROM Usuarios u JOIN Clientes c ON (u.id_usuario = c.id_usuario)
								  JOIN Puntos p ON (p.id_cliente = c.id_cliente)
	WHERE username = @username AND p.fecha_vencimiento > @fecha
END
