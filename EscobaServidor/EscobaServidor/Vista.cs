namespace EscobaServidor;

public abstract class Vista
{
    protected abstract void Escribir(string mensaje);
    protected abstract string LeerLinea();
    public virtual void Cerrar() {}

    protected void EscribirLinea(string mensaje) => Escribir(mensaje + "\n");
    protected void EscribirLinea() => EscribirLinea("");


    public void Pausar() => LeerLinea();

    public void MensajeBienvenida() => EscribirLinea("Bienvenido a Escoba!");

    public void MensajeEsperandoJugador2() => EscribirLinea("Esperando jugador 2...");

    public bool PedirModoDeJuego()
    {
        EscribirLinea("Ingrese 1 para jugar ONLINE, 2 para modo CONSOLA");
        int modo = PedirNumeroValido(1, 2);
        if (modo == 1)
        {
            EscribirLinea("Modo servidor seleccionado");
            return true;
        }
        else if (modo == 2)
        {
            EscribirLinea("Modo consola seleccionado");
            return false;
        }
        return false;
    }

    public void MostrarMano(Jugador jugador)
    {
        Escribir("\nESTA ES TU MANO: ");
        foreach (var carta in jugador.CartasMano)
            Escribir(carta.ToString());
        EscribirLinea();
    }

    public void MostrarPuntosFinMano(Jugadores jugadores)
    {   
        EscribirLinea("Ha terminado la Mano");
        EscribirLinea("PUNTOS:");
        for (int i = 0; i < jugadores.CantidadJugadores(); i++)
        {
            EscribirLinea($"Jugador " + jugadores.ObtenerJugador(i)._nombre + ": " + jugadores.ObtenerJugador(i)._puntosJuego + " puntos");
        }
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

    public int PedirJugada(List<List<Carta>> jugadas)
    {
        EscribirLinea("Hay " + jugadas.Count + " jugadas en la mesa");
        for (int i = 0; i < jugadas.Count; i++)
        {
            Escribir(i + ": ");
            foreach (var carta in jugadas[i])
                Escribir(carta.ToString());
            EscribirLinea();
        }
        EscribirLinea("¿Cuál quieres usar?");
        int idJugada = PedirNumeroValido(0, jugadas.Count - 1);
        return idJugada;
    }

    public void InformarQueNoExisteCombinacion(Jugador jugador) => EscribirLinea("No se arma ninguna suma de 15");

    public void MostrarMensajeCartasGanadas(Jugador jugador, List<Carta> cartasGanadas)
    {
        EscribirLinea("Jugador " + jugador.ToString() + " ganó las siguientes cartas:");
        foreach (var carta in cartasGanadas)
            EscribirLinea(carta.ToString());
    }

    public void InformarEscoba(Jugador jugador) => EscribirLinea("Escoba para " + jugador.ToString() + "! ---------------------------");

    public void MostrarMensajeFelicitandoGanador(Jugador ganador) => EscribirLinea("Felicidades " + ganador._nombre + "! Ganaste la partida con " + ganador._puntosJuego + " puntos");

    public void MostrarMensajePerdedor(Jugador ganador, Jugador perdedor) => EscribirLinea("Perdiste " + perdedor._nombre + "! " + ganador._nombre + " ganó la partida con " + ganador._puntosJuego + " puntos");

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