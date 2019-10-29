using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweens
{
	/**
		 * Easing equation float for an elastic (exponentially decaying sine wave) easing out: decelerating from zero velocity.
		 *
		 * @param t		Current time (in frames or seconds).
		 * @param b		Starting value.
		 * @param c		Change needed in value.
		 * @param d		Expected easing duration (in frames or seconds).
		 * @param a		Amplitude.
		 * @param p		Period.
		 * @return		The correct value.
		 */

	public static float EaseOutElastic (float t, float b, float c, float d, float a) {
		if (t==0) return b;
		if ((t/=d)==1) return b+c;
		float p = d*.3f;
		float s = 0;
		//float a = 0;
		if (a == 0f || a < Mathf.Abs(c)) {
			a = c;
			s = p/4;
		} else {
			s = p/(2*Mathf.PI) * Mathf.Asin (c/a);
		}
		return (a*Mathf.Pow(2,-10*t) * Mathf.Sin( (t*d-s)*(2*Mathf.PI)/p ) + c + b);
	}
		
	/**
		 * 
		 * @param t		Current time (in frames or seconds).
		 * @param d		Expected easing duration (in frames or seconds).
		 * @param o		Oryginal (previous) position.
		 * @param p		Period.
		 */

	public static float ShakeWave(float t, float d, float o, float p )
	{
		//Random ratio of the wave
		float rnd = (Random.value * 0.4f) - 0.2f;

		if ((t /= d) >= 1) 
		{
			return o;
		}

		return  (Mathf.Exp(-(10/d)*t)*Mathf.Cos(p*t)*rnd+o);
	}
}