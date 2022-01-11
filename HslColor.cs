using System;

/// <summary>
/// Class that represent a HSL color with an alpha component
/// </summary>
public class HslColor
{
    //Predefined colors for convenience sake
    public readonly static HslColor black = new HslColor(0.0, 0.0, 0.0, 1.0);
    public readonly static HslColor grey = new HslColor(0.0, 0.0, 0.5, 1.0);
    public readonly static HslColor gray = grey;
    public readonly static HslColor white = new HslColor(0.0, 0.0, 1.0, 1.0);
    public readonly static HslColor clear = new HslColor(0.0, 0.0, 0.0, 0.0);
    public readonly static HslColor red = new HslColor(0.0, 1.0, 0.5, 1.0);
    public readonly static HslColor green = new HslColor(120.0/360.0, 1.0, 0.5, 1.0);
    public readonly static HslColor blue = new HslColor(240.0/360.0, 1.0, 0.5, 1.0);
    public readonly static HslColor yellow = new HslColor(60.0/360.0, 1.0, 0.5, 1.0);
    public readonly static HslColor cyan = new HslColor(180.0/360.0, 1.0, 0.5, 1.0);
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
        this.start(hsla[0],hsla[1],hsla[2],hsla[3]);
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
            calculatedAngle = _angle + (2.0*Math.PI) *Math.Ceiling(Math.Abs(_angle/(2.0*Math.PI)));
        }else{
            calculatedAngle = -_angle + (2.0*Math.PI) * Math.Ceiling(_angle/(2.0*Math.PI));
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
    /// <returns>double - the luminance value</returns>
    public double getLuminance(){
        double[] rgba = this.getRgba();
        double luminance = 0.2126*rgba[0]+0.7152*rgba[1]+0.0722*rgba[2];
        return luminance;
    }

    //============================================ RGB ==============================================

    // HSL -> RGB
    /// <summary>  Converts from HSL to RGB as a array of double. The components are within the 0 to 1 range. </summary>
    /// <returns>double[4] with the RGBA components</returns>
    public double[] getRgba(){
        
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
    /// <summary> Sets this HSL color from the specified RGBA values. RGBA components must be between 0 and 1.  </summary>
    /// <param name="_r">Red component ∈ [0,1]</param>
    /// <param name="_g">Green component ∈ [0,1]</param>
    /// <param name="_b">Blue component ∈ [0,1]</param>
    /// <param name="_alpha">Alpha component ∈ [0,1]</param>
    public void fromRgba(double _r, double _g, double _b,double _alpha = 1.0){
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

    /// <summary> Sets this HSL color from the specified rgb values. RGBA components must be between 0 and 1. </summary>
    /// <param name="rgba">double[4] with all RGBA components between 0 and 1</param>
    public void fromRgba(double[] rgba){
        if(rgba.Length == 4){
            this.fromRgba(rgba[0],rgba[1],rgba[2],rgba[3]);
        }
        else if( rgba.Length == 3){
            this.fromRgba(rgba[0],rgba[1],rgba[2]);
        }else{
            throw new ArgumentException("Invalid argument for .fromRgba(). Argument must be double[4] or double[3].");
        }
    }

    /// <summary> Returns the RGBA hexcode (format: #RRGGBBAA) - 9 digits; 1 digit for '#' and 4 pairs of 2 digits for the RGBA components </summary>
    /// <returns>String in the format #RRGGBBAA</returns>
    public String getHex(){
        string outHex = "#";

        double[] rgba = this.getRgba();
        for(int i=0; i<4;i++){
            outHex += (Convert.ToInt32(rgba[i]*255)).ToString("X2");
        }
        
        return outHex;
    }

    //==================================== HSV ==========================================

    // HSL -> HSV
    /// <summary> Converts from HSL to HSV as a array of double with the hsva components between 0.0 and 1.0]. </summary>
    /// <returns>double[4] with the components HSVA</returns>
    public double[] getHsva(){
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
    public void fromHsva(double _h,double _s,double _v, double _alpha = 1.0){
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
    public void fromHsva(double[] hsva){
        if( hsva.Length == 4 ){
            this.fromHsva(hsva[0],hsva[1],hsva[2],hsva[3]);
        }else if( hsva.Length == 3){
            this.fromHsva(hsva[0],hsva[1],hsva[2]);
        }else{
            throw new ArgumentException("Invalid argument for .fromHsva(). Argument must be double[4] or double[3].");
        }
    }

    //=================================== STATIC =============================================

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

    //=============================== STATIC CONVERSION =======================================

    //HSL->HSV
    public static double[] HSLToHSV(HslColor input){
        return input.getHsva();
    }
    //HSV->HSL
    public static HslColor HSVToHSL(double _h, double _s, double _v, double _alpha=1.0){
        HslColor color = new HslColor();
        color.fromHsva(_h,_s,_v,_alpha);
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

    //HSL->RGB
    public static double[] HSLToRGB(HslColor input){
        return input.getRgba();
    }
    //RGB->HSL
    public static HslColor RGBToHSL(double _r, double _g, double _b, double _alpha=1.0){
        HslColor color = new HslColor();
        color.fromRgba(_r,_g,_b,_alpha);
        return color;
    }
    public static HslColor RGBToHSL(double[] c){
        if(c.Length == 4){
            return HslColor.RGBToHSL(c[0],c[1],c[2],c[3]);
        }else{
            if(c.Length == 3){
                return HslColor.RGBToHSL(c[0],c[1],c[2]);
            }else{
                throw new ArgumentException("HslColor.RGBToHSL: argument must be double[4] or double[3]");
            }
        }
    }

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

    // double[] = HslColor
    public static implicit operator double[](HslColor color) => color.toArray();
    // HslColor = double[]
    public static implicit operator HslColor(double[] color){
        return new HslColor(color);
    }

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