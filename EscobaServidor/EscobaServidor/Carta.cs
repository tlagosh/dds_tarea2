namespace EscobaServidor;

public class Carta
{
    private int _valor;
    private string _pinta;
    public int valor
    {
        get { return _valor; }
    }
    public string pinta
    {
        get { return _pinta; }
    }

    public Carta(int valor, string pinta)
    {
        _valor = valor;
        _pinta = pinta;
    }

    public bool EsSiete() => _valor == 7;
    
    public bool EsOro() => _pinta == "Oro";

    public override string ToString() => "[" + _valor + " de " + _pinta + "]";
    
}