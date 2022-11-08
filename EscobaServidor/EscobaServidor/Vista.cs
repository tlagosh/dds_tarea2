namespace EscobaServidor;

public abstract class Vista
{
    protected abstract void Escribir(string mensaje);
    protected abstract string LeerLinea();
    public virtual void Cerrar() {}

    protected void EscribirLinea(string mensaje) => Escribir(mensaje + "\n");
    protected void EscribirLinea() => EscribirLinea("");


    public void Pausar() => LeerLinea();

    public void MostrarMano(Jugador jugador)
    {
        Escribir("\nESTA ES TU MANO: ");
        foreach (var carta in jugador.CartasMano)
            Escribir(carta.ToString());
        EscribirLinea();
    }

    public void MostrarInfoJugador(Jugador jugador) => EscribirLinea("Juega " + jugador.ToString() + "---------------------------");

    public void MostrarMesa(CartasEnMesa cartasEnMesa) => EscribirLinea("MESA: " + cartasEnMesa);

    public int PedirCarta(Jugador jugador)
    {
        EscribirLinea("¿Cuál de las siguientes cartas quieres bajar?");
        for (int i = 0; i < jugador.CartasMano.Count; i++)
            EscribirLinea(i + ": " + jugador.CartasMano[i]);
        int idJugada = PedirNumeroValido(0, jugador.CartasMano.Count - 1);

        return idJugada;
    }

    public void MostrarMensajeCartasGanadas(Jugador jugador, List<Carta> cartasGanadas, bool escoba)
    {
        EscribirLinea("Jugador " + jugador.ToString() + " ganó las siguientes cartas:");
        foreach (var carta in cartasGanadas)
            EscribirLinea(carta.ToString());

        if (escoba)
            EscribirLinea("--------¡¡ESCOBA!!--------");
    }

    public void MostrarMensajeFelicitandoGanador(Jugador ganador)
    {
        //Felicitar Ganador
        EscribirLinea("Felicidades! Ganaste la partida con " + ganador._puntosJuego + " puntos");
    }

    private string ConvertirListaAString(List<int> lista)
    {
        string retorno = "[" + lista[0];
        for (int i = 1; i < lista.Count; i++)
            retorno += ", " + lista[i];
        retorno += "]";
        return retorno;
    }

    private int PedirNumeroValido(int minValue, int maxValue)
    {
        int numero;
        bool fuePosibleTransformarElString;
        do
        {
            string? inputUsuario = LeerLinea();
            fuePosibleTransformarElString = int.TryParse(inputUsuario, out numero);
        } while (!fuePosibleTransformarElString || numero < minValue || numero > maxValue);

        return numero;
    }

}