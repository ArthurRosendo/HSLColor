using System;
using System.Linq;

/// <summary>
/// Class that represent a HSL color with an alpha component
/// </summary>
public class HslColor
{
    //Predefined colors for convenience sake
    public readonly static HslColor black = new HslColor(0.0, 0.0, 0.0, 1.0);
    public readonly static HslColor gray = new HslColor(0.0, 0.0, 0.5, 1.0); // 50% gray
    public readonly static HslColor grey = HslColor.gray; //Alias for gray
    public readonly static HslColor white = new HslColor(0.0, 0.0, 1.0, 1.0);
    public readonly static HslColor clear = new HslColor(0.0, 0.0, 0.0, 0.0); // 100% Transparent black (0% alpha)
    public readonly static HslColor red = new HslColor(0.0, 1.0, 0.5, 1.0);
    public readonly static HslColor green = new HslColor(120.0/360.0, 1.0, 0.5, 1.0);
    public readonly static HslColor blue = new HslColor(240.0/360.0, 1.0, 0.5, 1.0);
    public readonly static HslColor yellow = new HslColor(60.0/360.0, 1.0, 0.5, 1.0);
    public readonly static HslColor cyan = new HslColor(180.0/360.0, 1.0, 0.5, 1.0); //As in CMYK, light blue (голубой)
    public readonly static HslColor magenta = new HslColor(300.0/360.0, 1.0, 0.5, 1.0);
    

    /// <summary> Hue component ∈ [0, 1] </summary>
    protected double h;

    /// <summary> Saturation component ∈ [0, 1] </summary>
    protected double s;

    /// <summary> Lightness component ∈ [0, 1] </summary>
    protected double l;

    /// <summary> Alpha component ∈ [0, 1] </summary>
    protected double alpha;

    /// <summary> Class constructor for HslColor, a representation of color in HSL space. </summary>
    /// <param name="_h">Hue between 0 and 1</param>
    /// <param name="_s">Saturation between 0 and 1</param>
    /// <param name="_l">Lightness between 0 and 1</param>
    /// <param name="_alpha">Alpha between 0 and 1</param>
    public HslColor(double _h = 0.0, double _s = 0.0, double _l = 0.0, double _alpha = 1.0){
        this.start(_h,_s,_l,_alpha);
    }

    /// <summary> Class constructor for HslColor, a representation of color in HSL space. </summary>
    /// <param name="hsla">Double[4] with all components of the HSLA color between 0 to 1</param>
    public HslColor(double[] hsla){

        //TODO: Surely there is a better way to write this in C#
        switch (hsla.Length)
        {
            case 4:
                this.start(hsla[0],hsla[1],hsla[2],hsla[3]);
                break;
            case 3:
                this.start(hsla[0],hsla[1],hsla[2]);
                break;
            case 2:
                this.start(hsla[0],hsla[1]);
                break;
            case 1:
                this.start(hsla[0]);
                break;
            case 0:
                this.start();
                break;
            default:
                throw new ArgumentException("Argument must be an array with 4 elements or less");
        }

    }

    /// <summary> Method used by all constructors to instantiate the fields </summary>
    /// <param name="_h">Hue between 0 and 1</param>
    /// <param name="_s">Saturation between 0 and 1</param>
    /// <param name="_l">Lightness between 0 and 1</param>
    /// <param name="_alpha">Alpha between 0 and 1</param>
    protected void start(double _h = 0.0, double _s = 0.0, double _l = 0.0, double _alpha = 1.0){
        checkComponents(_h,_s,_l,_alpha, "HslColor() constructor");
        
        this.h = _h;
        this.s = _s;
        this.l = _l;
        this.alpha = _alpha;
    }

    //Hue
    public double getHue(){
        return this.h;
    }
    public void setHue(double value){
        this.h = Math.Ceiling(Math.Abs(value)) - Math.Abs(value);
    }

    //Saturation
    public double getSaturation(){
        return this.s;
    }
    public void setSaturation(double value){
        this.s = Math.Clamp(value, 0.0, 1.0);
    }

    //Lightness
    public double getLightness(){
        return this.l;
    }
    public void setLightness(double value){
        this.l = Math.Clamp(value, 0.0, 1.0);
    }

    //Alpha
    public double getAlpha(){
        return this.alpha;
    }
    public void setAlpha(double value){
        this.alpha = Math.Clamp(value, 0.0, 1.0);
    }

    /// <summary> Returns an array with the conponents of this HslColor  </summary>
    /// <returns>double[4] with the HSLA components of this object</returns>
    public double[] toArray(){
        Double[] outArray = {this.h, this.s, this.l, this.alpha};
        return outArray;
    }

    public override string ToString()
    {
        return "(" + this.h + " ; " + this.s + " ; " + this.l + " ; " + this.alpha + ")";
    }

    /// <summary> Sets the hue from the given angle in degrees to a 0 to 1 range. </summary>
    /// <param name="_angle">The input angle in degrees</param>
    public void setHueAngleDegrees(double _angle){
        double calculatedAngle;
        //All of this ensures that it works with negative angles and angles over 360 degrees
        if(_angle < 0){
            calculatedAngle = _angle + 360.0 *Math.Ceiling(Math.Abs(_angle/360.0));
        }else{
            calculatedAngle = -_angle + 360.0 *Math.Ceiling(_angle/360.0);
        }

        this.h =  calculatedAngle / 360.0;
    }

    /// <summary> Sets the hue from the given angle in radians to a 0 to 1 range. </summary>
    /// <param name="_angle">The input angle in radians</param>
    public void setHueAngleRadians(double _angle){
        double calculatedAngle;
        //All of this ensures that it works with negative angles and angles over 2pi (360 degrees)
        if(_angle < 0){
            calculatedAngle =  _angle + ( 2.0*Math.PI * Math.Ceiling(Math.Abs(_angle/(2.0*Math.PI))) );
        }else{
            calculatedAngle = 2.0*Math.PI * (_angle/(2.0*Math.PI) - Math.Floor(_angle/(2.0*Math.PI)));   
        }
        
        
        this.h =  calculatedAngle / (2.0*Math.PI);
    }

    /// <summary> Returns the hue as an angle in degrees </summary>
    /// <returns>double - the angle in degrees</returns>
    public double getHueAngle(){
        return this.h * 360.0;
    }

    /// <summary> Return the hue as an angle in radians </summary>
    /// <returns>double - the angle in radians</returns>
    public double getHueRadians(){
        return this.h * (2.0*Math.PI);
    }

    /// <summary> Return the perceptual luminance of the color. (ITU BT.709) </summary>
    /// The Luma differs from Luminance in that the Luma is calculated from the Gamma weighted (sRGB) values, while Luminance is from linear
    /// <returns>double - the luminance value</returns>
    public double getLuminance(){
        double[] rgba = this.getRGBA();
        double luminance = 0.2126*rgba[0]+0.7152*rgba[1]+0.0722*rgba[2];
        return luminance;
    }

    /// <summary> Return the Luma of the color. (ITU BT.709) </summary>
    /// The Luma differs from Luminance in that the Luma is calculated from the Gamma weighted (sRGB) values, while Luminance is from linear
    /// <returns>double - the luminance value</returns>
    public double getLuma(){
        HslColor output = new HslColor();
        output.fromRGBA( this.getSRGBA() );
        return output.getLuminance();
    }

    /// <summary>
    /// Returns the inverted color, the alpha component remains unchanged
    /// </summary>
    /// <returns>The inverted HslColor</returns>
    public HslColor Invert(){
        double h = 1.0 - this.h;
        double s = 1.0 - this.s;
        double l = 1.0 - this.l;
        return new HslColor(h,s,l,this.alpha);
    }

    #region === sRGB ===

    /// <summary>
    /// Returns the sRGBA value of this HSL color
    /// </summary>
    /// <returns>double[4] with sRGBA values</returns>
    public double[] getSRGBA(){
        double[] rgba = this.getRGBA();
        double sr = sRGBComponent(rgba[0]);
        double sg = sRGBComponent(rgba[1]);
        double sb = sRGBComponent(rgba[2]);
        //Alpha is linear, no need to be computed

        return new HslColor(sr,sg,sb,this.alpha);
    }

    /// <summary>
    /// Sets the value of this HslColor to the specified sRGBA color
    /// </summary>
    /// <param name="_srgba">the sRGBA color to set from. Must be a double[3] or double[4]</param>
    public void fromSRGBA(double[] _srgba){
        if(_srgba.Length == 4){
            this.fromSRGBA(_srgba[0],_srgba[1],_srgba[2],_srgba[3]);
        }else{
            if(_srgba.Length == 3){
                this.fromSRGBA(_srgba[0],_srgba[1],_srgba[2]);
            }
        }
    }

    /// <summary>
    /// Sets the value of this HslColor to the specified sRGBA color. If alpha is not specified then it is set to 1.0.
    /// </summary>
    /// <param name="_sr">red component of the sRGBA input</param>
    /// <param name="_sg">green component of the sRGBA input</param>
    /// <param name="_sb">blue component of the sRGBA input</param>
    /// <param name="_a">Optional. The alpha component of the sRGBA input</param>
    public void fromSRGBA(double _sr, double _sg, double _sb, double _a = 1.0){
        double r = linearComponent(_sr);
        double g = linearComponent(_sg);
        double b = linearComponent(_sb);
        double a = _a;

        this.fromRGBA(r,g,b,a);
    }

    /// Converts given RGB component to sRGB
    protected double sRGBComponent(double _component){
        //Altenative cutoff value is 0.00313066844250063
        if (_component <= 0.00031308)
            return 12.92 * _component;
        else
            return 1.055*Math.Pow(_component,(1.0 / 2.4) ) - 0.055;
    }

    /// Converts given sRGB component to RGB
    protected double linearComponent(double _weightedComponent){
        //Alternative cutoff point 0.0404482362771082
        if(_weightedComponent <= 0.04045){
            return _weightedComponent/12.92;
        }else{
            return Math.Pow( (_weightedComponent+0.055)/1.055, 2.4 );
        }
    }
    #endregion

    #region === RGB  ===

    // HSL -> RGB
    /// <summary>  Converts from HSL to RGB as a array of double. The components are within the 0 to 1 range. </summary>
    /// <returns>double[4] with the RGBA components</returns>
    public double[] getRGBA(){
        
        double c = (1.0 - Math.Abs(2.0*this.l-1.0)) * this.s;
        double x = c * ( 1.0-Math.Abs((this.getHueAngle()/60.0)%2.0 - 1.0) );
        double m = this.l - c/2.0;

        double rp, gp, bp;
        
        if(this.h >= 0.0 && this.h <= 60.0/360.0){
            rp = c;
            gp = x;
            bp = 0.0;
        }else if( this.h <= 120.0/360.0 ){
            rp = x;
            gp = c;
            bp = 0.0;
        }else if( this.h <= 180.0/360.0 ){
            rp = 0.0;
            gp = c;
            bp = x;
        }else if( this.h <= 240.0/360.0 ){
            rp = 0.0;
            gp = x;
            bp = c;
        }else if( this.h <= 300.0/360.0 ){
            rp = x;
            gp = 0.0;
            bp = c;
        }else if( this.h <= 360.0/360.0 ){
            rp = c;
            gp = 0.0;
            bp = x;
        } else{
            throw new InvalidOperationException("Hue is outside of range [0.0,1.0]: "+this.h.ToString());
        }

        rp += m;
        gp += m;
        bp += m;

        double[] doubleOut = { rp , gp , bp , this.alpha };
        return doubleOut;
    }

    // RGB -> HSL
    /// <summary> Sets this HSL color from the specified RGBA values. If alpha is not specified then it is set to 1.0.  </summary>
    /// <param name="_r">Red component ∈ [0,1]</param>
    /// <param name="_g">Green component ∈ [0,1]</param>
    /// <param name="_b">Blue component ∈ [0,1]</param>
    /// <param name="_alpha">Alpha component ∈ [0,1]</param>
    public void fromRGBA(double _r, double _g, double _b,double _alpha = 1.0){
        checkComponents(_r,_g,_b,_alpha, "HslColor.fromRgba()");

        double cmax =  Math.Max(Math.Max(_r,_g),_b);
        double cmin =  Math.Min(Math.Min(_r,_g),_b);
        double delta = cmax - cmin;

        //HUE (H)
        if(delta == 0.0){
            this.h = 0.0;
        }else if(cmax == _r){
            setHueAngleDegrees( 60.0/360.0 * ((_g-_b)/delta%6f) );
        }else if(cmax == _g){
            setHueAngleDegrees( 60.0/360.0 * ((_b-_r)/delta+2.0) );
        }else if(cmax == _b){
            setHueAngleDegrees( 60.0/360.0 * ((_r-_g)/delta+4f) );
        }

        //LIGHTNESS (L)
        double lightness = (cmax+cmin)/2.0;
        this.l = lightness;

        //SATURATION (S)
        if(delta == 0.0){
            this.s = 0.0;
        }else{
            this.s = delta/(1.0-Math.Abs(2.0*lightness-1.0));
        }

        this.alpha = _alpha;
    }

    /// <summary> Sets this HSL color from the specified rgb values. If alpha is not specified then it is set to 1.0.</summary>
    /// <param name="rgba">double[4] with all RGBA components between 0 and 1</param>
    public void fromRGBA(double[] rgba){
        if(rgba.Length == 4){
            this.fromRGBA(rgba[0],rgba[1],rgba[2],rgba[3]);
        }
        else if( rgba.Length == 3){
            this.fromRGBA(rgba[0],rgba[1],rgba[2]);
        }else{
            throw new ArgumentException("Invalid argument for .fromRgba(). Argument must be double[4] or double[3].");
        }
    }

    /// <summary> Returns the RGBA hexcode (format: #RRGGBBAA) - 9 digits; 1 digit for '#' and 4 pairs of 2 digits for the RGBA components </summary>
    /// <returns>String in the format #RRGGBBAA</returns>
    public String getHex(){
        string outHex = "#";

        double[] rgba = this.getRGBA();
        for(int i=0; i<4;i++){
            outHex += (Convert.ToInt32(rgba[i]*255)).ToString("X2");
        }
        
        return outHex;
    }
    #endregion

    #region === HSV  ===

    // HSL -> HSV
    /// <summary> Converts from HSL to HSV as a array of double with the hsva components between 0.0 and 1.0]. </summary>
    /// <returns>double[4] with the components HSVA</returns>
    public double[] getHSVA(){
        double v = this.l+this.s*Math.Min(this.l,1-this.l);
        double sv;
        if(v == 0.0){
            sv = 0.0;
        }else{
            sv = 2.0*(1.0-(this.l/v));
        }

        double[] outHsv = {this.h, sv, v, this.alpha};
        return outHsv;
    }

    // HSV -> HSL
    /// <summary> Sets this color from a HSV input </summary>
    /// <param name="_h">HSV hue ∈ [0,1]</param>
    /// <param name="_s">HSV saturation ∈ [0,1]</param>
    /// <param name="_v">HSV value ∈ [0,1]</param>
    /// <param name="_alpha">alpha ∈ [0,1]. Optional, default is 1.0</param>
    public void fromHSVA(double _h,double _s,double _v, double _alpha = 1.0){
        checkComponents(_h,_s,_v,_alpha, "HslColor.fromHsva()");

        this.h = _h;

        this.l = _v*(1.0-(_s/2.0));

        if(this.l == 0.0 || this.l == 0.0){
            this.s = 0.0;
        }else{
            this.s = (_v-this.l)/Math.Min(this.l,1.0-this.l);
        }

        this.alpha = _alpha;
    }

    /// <summary> Sets this color from a HSV input. The arguments must be in the following order: h, s, v, ?alpha </summary>
    /// <param name="hsva">double[4] or double[3] with HSVA or HSV components between 0 to 1</param>
    public void fromHSVA(double[] hsva){
        if( hsva.Length == 4 ){
            this.fromHSVA(hsva[0],hsva[1],hsva[2],hsva[3]);
        }else if( hsva.Length == 3){
            this.fromHSVA(hsva[0],hsva[1],hsva[2]);
        }else{
            throw new ArgumentException("Invalid argument for .fromHsva(). Argument must be double[4] or double[3].");
        }
    }

    //==================================== HSB (alias) ==========================================

    /// <summary> Converts from HSL to HSB as a array of double with the hsva components between 0.0 and 1.0]. </summary>
    /// <returns>double[4] with the components HSBA</returns>
    public double[] getHSBA(){
        return this.getHSVA();
    }

    /// <summary> Sets this color from a HSB input </summary>
    /// <param name="_h">HSB hue ∈ [0,1]</param>
    /// <param name="_s">HSB saturation ∈ [0,1]</param>
    /// <param name="_b">HSB value ∈ [0,1]</param>
    /// <param name="_alpha">alpha ∈ [0,1]. Optional, default is 1.0</param>
    public void fromHSBA(double _h,double _s,double _b, double _alpha = 1.0){
        this.fromHSVA(_h,_s,_b,_alpha);
    }

    /// <summary> Sets this color from a HSB input. The arguments must be in the following order: h, s, b, ?alpha </summary>
    /// <param name="hsba">double[4] or double[3] with HSBA or HSB components between 0 to 1</param>
    public void fromHSBA(double[] hsba){
        this.fromHSVA(hsba);
    }
    #endregion

    #region === Bicone ===
    
    /// <summary>
    /// Returns the bicone value (still in cylinder space) of this HSL color
    /// </summary>
    /// <returns>double[4] with bicone HSL values</returns>
    public double[] getBicone(){
        double[] rgba = this.getRGBA();
        double[] rgb = {0,0,0};
        Array.Copy(rgba,rgb,3);
        double c = rgb.Max() - rgb.Min();
        
        return new HslColor(this.h,c,this.l,this.alpha);
    }

    /// <summary>
    /// Sets from the values of a HSL bicone in cylindrical space
    /// </summary>
    /// <param name="_h">Hue. 0 to 1.</param>
    /// <param name="_c">The "chroma" (Joblove and Greenberg (1978)). 0 to 1</param>
    /// <param name="_l">The lightness. 0 to 1</param>
    /// <param name="_alpha">(optional) Alpha. 0 to 1</param>
    /// <returns></returns>
    public void fromBicone(double _h, double _c, double _l, double _alpha=1.0){

        if(_l == 0 || l == 1){
            this.s = 0;
            return;
        }

        this.h = _h;
        this.s = _c / Math.Max(-2*Math.Abs(_l-0.5)+1, 0.0);
        this.l = _l;
        this.alpha = _alpha;
    }

    /// <summary>
    /// Sets from Bicone
    /// </summary>
    /// <param name="_hcla">double[4] or double[3] with hue, chroma, lightness and alpha(optionally)</param>
    public void fromBicone(double[] _hcla){
        if( _hcla.Length == 4 ){
            this.fromBicone(_hcla[0],_hcla[1],_hcla[2],_hcla[3]);
        }else if( _hcla.Length == 3){
            this.fromBicone(_hcla[0],_hcla[1],_hcla[2]);
        }else{
            throw new ArgumentException("Invalid argument for .fromBicone(). Argument must be double[4] or double[3].");
        }

    }
    #endregion

    //=================================== STATIC =============================================

    public static HslColor random(){
        Random rng = new Random();
        HslColor output = new HslColor();
        //The reason I use RGB is to simplify the random operation since HSL allows more than 1 set of component values per color (eg.: HSL[0,0,0] and HSL[0,1,0] are both black)
        output.fromRGBA(NextDouble01(),NextDouble01(),NextDouble01());
        return output;
    }

    /// <summary>
    /// Returns a random number between 0 and 1 inclusively
    /// </summary>
    /// <returns>Double between 0 and 1 inclusively</returns>
    protected static double NextDouble01()
    {
        Random rand = new Random();
        double minimum = 0;
        double maximum = 1.0000000004656612873077392578125; //This is a hack to make the range inclusively
        return rand.NextDouble() * (maximum - minimum) + minimum;
    }

    /// <summary> Interpolates two HslColor together to form a new HslColor. </summary>
    /// <param name="ha">The first color</param>
    /// <param name="hb">The second color</param>
    /// <param name="w"> The weight of the interpolation ∈ [0,1]. Optional, default is 0.5.</param>
    /// <returns></returns>
    public static HslColor interpolateHsl(HslColor ha, HslColor hb, double w = 0.5){
        if(w < 0 || w > 1){
            throw new ArgumentException("3rd argument of interpolateHsl() is not within the range [0.0,1.0]");
        }

        //Saturation, lightness and alpha interpolation
        double ns =  ha.s * (1.0-w) + hb.s * w;
        double nl =  ha.l * (1.0-w) + hb.l * w;
        double na =  ha.alpha * (1.0-w) + hb.alpha * w;

        //Hue - angle interpolation
        
        double[] hx = {  Math.Cos(ha.getHueRadians()) , Math.Sin(ha.getHueRadians())  };
        double[] hy = {  Math.Cos(hb.getHueRadians()) , Math.Sin(hb.getHueRadians())  };
        double[] ht = { (hx[0]+hy[0])*w , (hx[1]+hy[1])*w }; 
        double nh = Math.Atan(ht[1]/ht[0]);

        //Output
        HslColor hslOut = new HslColor();
        hslOut.setHueAngleRadians(nh);
        hslOut.s = ns;
        hslOut.l = nl;
        hslOut.alpha = na;
        return hslOut;
    }

    #region === STATIC CONVERSION ===

    //HSL->HSV
    public static double[] HSLToHSV(HslColor input){
        return input.getHSVA();
    }
    //HSB alias
    public static double[] HSLToHSB(HslColor input){
        return input.getHSVA();
    }
    
    //HSV->HSL
    public static HslColor HSVToHSL(double _h, double _s, double _v, double _alpha=1.0){
        HslColor color = new HslColor();
        color.fromHSVA(_h,_s,_v,_alpha);
        return color;
    }
    public static HslColor HSVToHSL(double[] c){
        if(c.Length == 4){
            return HslColor.HSVToHSL(c[0],c[1],c[2],c[3]);
        }else{
            if(c.Length == 3){
                return HslColor.HSVToHSL(c[0],c[1],c[2]);
            }else{
                throw new ArgumentException("HslColor.HSVToHSL: argument must be double[4] or double[3]");
            }
        }
    }
    //HSB alias
    public static HslColor HSBToHSL(double _h, double _s, double _b, double _alpha=1.0){
        return HslColor.HSVToHSL(_h,_s,_b,_alpha);
    }
    public static HslColor HSBToHSL(double[] c){
        return HslColor.HSVToHSL(c);
    }

    //HSL->RGB
    public static double[] HSLToRGB(HslColor input){
        return input.getRGBA();
    }

    //RGB->HSL
    public static HslColor RGBToHSL(double _r, double _g, double _b, double _alpha=1.0){
        HslColor color = new HslColor();
        color.fromRGBA(_r,_g,_b,_alpha);
        return color;
    }
    public static HslColor RGBToHSL(double[] _c){
        HslColor color = new HslColor();
        color.fromRGBA(_c);
        return color;
    }

    //HSL->sRGB
    public double[] HSLToSRGB(HslColor _input){
        return _input.getSRGBA();
    }

    //sRGB->HSL
    public HslColor HSLToSRGB(double _sr, double _sg, double _sb, double _a=1.0){
        HslColor color = new HslColor();
        color.fromSRGBA(_sr,_sg,_sb,_a);
        return color;
    }
    public HslColor HSLToSRGB(double[] _srgba){
        HslColor color = new HslColor();
        color.fromSRGBA(_srgba);
        return color;
    }
    
    #endregion

    // =============== Exceptions for the components =============================

    /// <summary> Throws exceptions if arguments are outside the 0 to 1 range </summary>
    /// <param name="_x">1st component</param>
    /// <param name="_y">2nd component</param>
    /// <param name="_z">3rd component</param>
    /// <param name="_w">Weight</param>
    /// <param name="sourceMsg">A string to help identify where the error is comming from.</param>
    protected static void checkComponents(double _x, double _y, double _z, double _w, String sourceMsg=""){
        if(_x < 0 || _y < 0 || _z < 0 || _w < 0 || _x > 1 || _y > 1 || _z > 1 || _w > 1){
            if(sourceMsg != null && sourceMsg != ""){
                throw new ArgumentException("HslColor" + ": " + sourceMsg + " :: Invalid value for one of the components. Components must be between 0 and 1.");
            }else{
                throw new ArgumentException("HslColor" + ": Invalid value for one of the components. Components must be between 0 and 1.");
            }
        }
    }

    // =======================================================================
    // OPERATORS AND ASSIGMENTS
    // =======================================================================

    // double[] a <- HslColor
    public static implicit operator double[](HslColor color) => color.toArray();
    // HslColor a <- double[]
    public static implicit operator HslColor(double[] color){
        return new HslColor(color);
    }

    //Acessor
    public double this[int index]{
        get{
            switch(index){
                case 0:
                    return this.h;
                case 1:
                    return this.s;
                case 2:
                    return this.l;
                case 3:
                    return this.alpha;
                default:
                    throw new System.IndexOutOfRangeException();
            }
        }
        set{
            switch(index){
                case 0:
                    this.h = value;
                    break;
                case 1:
                    this.s = value;
                    break;
                case 2:
                    this.l = value;
                    break;
                case 3:
                    this.alpha = value;
                    break;
                default:
                    throw new System.IndexOutOfRangeException();
            }
        }
    }

    //Operations
    /// <summary> Component-wise addition </summary>
    /// <param name="h1"></param>
    /// <param name="h2"></param>
    /// <returns></returns>
    public static HslColor operator + (HslColor h1, HslColor h2){
        double _h = h1.h + h2.h;
        _h = Math.Clamp(_h,0.0,1.0);
        double _s = h1.s + h2.s;
        _s = Math.Clamp(_s,0.0,1.0);
        double _l = h1.l + h2.l;
        _l = Math.Clamp(_l,0.0,1.0);
        double _alpha = h1.alpha + h2.alpha;
        _alpha = Math.Clamp(_alpha,0.0,1.0);
        return new HslColor(_h,_s,_l,_alpha);
    }
    /// <summary> Component-wise subtraction </summary>
    /// <param name="h1"></param>
    /// <param name="h2"></param>
    /// <returns></returns>
    public static HslColor operator - (HslColor h1, HslColor h2){
        double _h = h1.h - h2.h;
        _h = Math.Clamp(_h,0.0,1.0);
        double _s = h1.s - h2.s;
        _s = Math.Clamp(_s,0.0,1.0);
        double _l = h1.l - h2.l;
        _l = Math.Clamp(_l,0.0,1.0);
        double _alpha = h1.alpha - h2.alpha;
        _alpha = Math.Clamp(_alpha,0.0,1.0);
        return new HslColor(_h,_s,_l,_alpha);
    }
    /// <summary> Component-wise division </summary>
    /// <param name="h1"></param>
    /// <param name="h2"></param>
    /// <returns></returns>
    public static HslColor operator / (HslColor h1, HslColor h2){
        if(h2.h == 0){
            throw new DivideByZeroException("Division by zero. Hue component of HslColor division is zero.");
        }
        if(h2.s == 0){
            throw new DivideByZeroException("Division by zero. Saturation component of HslColor division is zero.");
        }
        if(h2.l == 0){
            throw new DivideByZeroException("Division by zero. Lightness component of HslColor division is zero.");
        }
        if(h2.alpha == 0){
            throw new DivideByZeroException("Division by zero. Alpha component of HslColor division is zero.");
        }
        double _h = h1.h / h2.h;
        _h = Math.Clamp(_h,0.0,1.0);
        double _s = h1.s / h2.s;
        _s = Math.Clamp(_s,0.0,1.0);
        double _l = h1.l / h2.l;
        _l = Math.Clamp(_l,0.0,1.0);
        double _alpha = h1.alpha / h2.alpha;
        _alpha = Math.Clamp(_alpha,0.0,1.0);
        return new HslColor(_h,_s,_l,_alpha);
    }
    /// <summary> Component-wise multiplication </summary>
    /// <param name="h1"></param>
    /// <param name="h2"></param>
    /// <returns></returns>
    public static HslColor operator * (HslColor h1, HslColor h2){
        double _h = h1.h * h2.h;
        _h = Math.Clamp(_h,0.0,1.0);
        double _s = h1.s * h2.s;
        _s = Math.Clamp(_s,0.0,1.0);
        double _l = h1.l * h2.l;
        _l = Math.Clamp(_l,0.0,1.0);
        double _alpha = h1.alpha * h2.alpha;
        _alpha = Math.Clamp(_alpha,0.0,1.0);
        return new HslColor(_h,_s,_l,_alpha);
    }

}
