namespace EscobaServidor;

public class Mano
{
    private const int IdUsuario = 0;
    private const int CantidadInicialCartas = 3;

    private Jugadores _jugadores;
    private int _idJugadorTurno;
    private CartasEnMesa _cartasEnMesa;

    private PilaCartas _pilaCartas = new PilaCartas();

    private Vista _vista = new VistaConsola();
    //private Vista _vista = new VistaSocket();

    public Mano(Jugadores _jugadores)
    {
        this._jugadores = _jugadores;
        PonerMesaVacia();
        RepartirCartas();
        Poner4CartasEnMesa();
        DecidirQuienParte();
    }

    private void PonerMesaVacia() => _cartasEnMesa = new CartasEnMesa();
    private void RepartirCartas() => _jugadores.RepartirCartas(CantidadInicialCartas, _pilaCartas);
    private void DecidirQuienParte() => _idJugadorTurno = GeneradorNumerosAleatorios.Generar(_jugadores.CantidadJugadores() - 1);

    private void Poner4CartasEnMesa()
    {
        for (int i = 0; i < 4; i++)
            _cartasEnMesa.Agregar(_pilaCartas.SacarCartaAlAzar());
    }
    
        
    public void Jugar()
    {
        while (!EsFinMano())
        {
            JugarTurno();
            _vista.Pausar();
        }
    }

    private bool EsFinMano()
    {
        bool noMasCartasEnPila = !_pilaCartas.TieneCartas();
        bool alguienGano = _jugadores.ExisteJugadorCon16Puntos();
        return noMasCartasEnPila || alguienGano;
    }

    private void JugarTurno()
    {
        _vista.MostrarInfoJugador(_jugadores.ObtenerJugador(_idJugadorTurno));
        _vista.MostrarMesa(_cartasEnMesa);
        _vista.MostrarMano(_jugadores.ObtenerJugador(_idJugadorTurno));
        int cartaParaBajar = _vista.PedirCarta(_jugadores.ObtenerJugador(_idJugadorTurno));
        BajarCarta(cartaParaBajar);
        _vista.MostrarMesa(_cartasEnMesa);
        AvanzarTurno();
    }

    private bool BajarCarta(int cartaParaBajar)
    {
        Carta carta = _jugadores.ObtenerJugador(_idJugadorTurno).CartasMano[cartaParaBajar];
        _jugadores.ObtenerJugador(_idJugadorTurno).CartasMano.RemoveAt(cartaParaBajar);

        (List<Carta> cartasGanadas, bool escoba) = _cartasEnMesa.ObtenerCartasGanadas(carta);

        if (cartasGanadas.Count > 0)
        {
            foreach (Carta cartaGanada in cartasGanadas)
            {
                _jugadores.ObtenerJugador(_idJugadorTurno).AgregarCartaAMano(cartaGanada);
            }
            if (escoba)
                _jugadores.ObtenerJugador(_idJugadorTurno)._puntosJuego += 1;
            _vista.MostrarMensajeCartasGanadas(_jugadores.ObtenerJugador(_idJugadorTurno), cartasGanadas, escoba);
            return true;

        }
        else
        {
            _cartasEnMesa.Agregar(carta);
            return false;
        }
    }

    private void AvanzarTurno() => _idJugadorTurno = (_idJugadorTurno+1) % 2;
}