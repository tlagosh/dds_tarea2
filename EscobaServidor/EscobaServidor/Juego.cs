namespace EscobaServidor;

public class Juego
{
    private const int NumJugadores = 2;
    private const int CantidadInicialCartas = 3;

    private Jugadores _jugadores;
    private int _idJugadorTurno;

    private int _idGanador = 0;
    private CartasEnMesa _cartasEnMesa;
    private Vista _vista = new VistaConsola();
    //private Vista _vista = new VistaSocket();

    public Juego()
    {
        CrearJugadores();
    }
    private void CrearJugadores() => _jugadores = new Jugadores(NumJugadores, new List<string> { "Jugador 1", "Jugador 2" });

    public void Jugar()
    {
        while (!EsFinJuego())
        {
            JugarMano();
        }
        DefinirGanador();
        FelicitarGanador();
        _vista.Cerrar();
    }

    private bool EsFinJuego()
    {
        return _jugadores.ExisteJugadorCon16Puntos();
    }

    private void JugarMano()
    {
        Mano mano = new Mano(_jugadores);
        mano.Jugar();
    }

    private void DefinirGanador()
    {
        //TODO: implementar
    }
    
    private void FelicitarGanador()
    {
        _vista.MostrarMensajeFelicitandoGanador(_jugadores.ObtenerJugador(_idGanador));
        //TODO: implementar       
    }
}