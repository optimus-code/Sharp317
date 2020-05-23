using System;
using System.Collections.Generic;
using System.Text;

namespace Sharp317
{
	public static class ISAACRandomGeneratorExtensions
	{
		//Extension method to support switching between RS2Sharp implementations and the renamed 317refactor.
		public static int getNextKey( this ISAACRandomGenerator generator )
		{
			return generator.value();
		}
	}

	/// <summary>
	/// An implementation of the ISAAC cipher
	///
	/// Based on Rand.java by Bob Jenkins
	///
	/// http://burtleburtle.net/bob/java/rand/Rand.java
	/// </summary>
	public class ISAACRandomGenerator
	{

		/**
		 * log of size of results[] and memory[]
		 */
		private readonly static int SIZEL = 8;

		/**
		 * size of results[] and memory[]
		 */
		private readonly static int SIZE = 1 << SIZEL;

		/**
		 * for pseudorandom lookup
		 */
		private readonly static int MASK = ( SIZE - 1 ) << 2;

		private int count;
		private readonly int[] results;
		private readonly int[] memory;
		private int accumulator;
		private int lastResult;
		private int counter;

		/// <summary>
		/// Create a new ISAAC generator with a given seed.
		/// </summary>
		/// <param name="seed">The generation seed.</param>
		public ISAACRandomGenerator( int[] seed )
		{
			this.memory = new int[SIZE];
			this.results = new int[SIZE];
			System.Buffer.BlockCopy( seed, 0, this.results, 0, sizeof( int ) * seed.Length );
			this.initialise();
		}

		/// <summary>
		/// Initialise or reinitialise the generator.
		/// </summary>
		private void initialise( )
		{
			int a, b, c, d, e, f, g, h;
			a = b = c = d = e = f = g = h = unchecked(( int ) 0x9e3779b9);
			for ( int i = 0; i < 4; i++ )
			{
				a ^= b << 11;
				d += a;
				b += c;
				b ^= ( int ) ( ( uint ) c >> 2 );
				e += b;
				c += d;
				c ^= d << 8;
				f += c;
				d += e;
				d ^= ( int ) ( ( uint ) e >> 16 );
				g += d;
				e += f;
				e ^= f << 10;
				h += e;
				f += g;
				f ^= ( int ) ( ( uint ) g >> 4 );
				a += f;
				g += h;
				g ^= h << 8;
				b += g;
				h += a;
				h ^= ( int ) ( ( uint ) a >> 9 );
				c += h;
				a += b;
			}

			for ( int i = 0; i < SIZE; i += 8 )
			{
				a += this.results[i];
				b += this.results[i + 1];
				c += this.results[i + 2];
				d += this.results[i + 3];
				e += this.results[i + 4];
				f += this.results[i + 5];
				g += this.results[i + 6];
				h += this.results[i + 7];
				a ^= b << 11;
				d += a;
				b += c;
				b ^= ( int ) ( ( uint ) c >> 2 );
				e += b;
				c += d;
				c ^= d << 8;
				f += c;
				d += e;
				d ^= ( int ) ( ( uint ) e >> 16 );
				g += d;
				e += f;
				e ^= f << 10;
				h += e;
				f += g;
				f ^= ( int ) ( ( uint ) g >> 4 );
				a += f;
				g += h;
				g ^= h << 8;
				b += g;
				h += a;
				h ^= ( int ) ( ( uint ) a >> 9 );
				c += h;
				a += b;
				this.memory[i] = a;
				this.memory[i + 1] = b;
				this.memory[i + 2] = c;
				this.memory[i + 3] = d;
				this.memory[i + 4] = e;
				this.memory[i + 5] = f;
				this.memory[i + 6] = g;
				this.memory[i + 7] = h;
			}

			for ( int i = 0; i < SIZE; i += 8 )
			{
				a += this.memory[i];
				b += this.memory[i + 1];
				c += this.memory[i + 2];
				d += this.memory[i + 3];
				e += this.memory[i + 4];
				f += this.memory[i + 5];
				g += this.memory[i + 6];
				h += this.memory[i + 7];
				a ^= b << 11;
				d += a;
				b += c;
				b ^= ( int ) ( ( uint ) c >> 2 );
				e += b;
				c += d;
				c ^= d << 8;
				f += c;
				d += e;
				d ^= ( int ) ( ( uint ) e >> 16 );
				g += d;
				e += f;
				e ^= f << 10;
				h += e;
				f += g;
				f ^= ( int ) ( ( uint ) g >> 4 );
				a += f;
				g += h;
				g ^= h << 8;
				b += g;
				h += a;
				h ^= ( int ) ( ( uint ) a >> 9 );
				c += h;
				a += b;
				this.memory[i] = a;
				this.memory[i + 1] = b;
				this.memory[i + 2] = c;
				this.memory[i + 3] = d;
				this.memory[i + 4] = e;
				this.memory[i + 5] = f;
				this.memory[i + 6] = g;
				this.memory[i + 7] = h;
			}

			this.isaac();
			this.count = SIZE;
		}

		/**
		 * Generate 256 random results.
		 */
		private void isaac( )
		{
			int a, b, x, y;

			this.lastResult += ++this.counter;
			for ( a = 0, b = SIZE / 2; a < SIZE / 2; )
			{
				x = this.memory[a];
				this.accumulator ^= this.accumulator << 13;
				this.accumulator += this.memory[b++];
				this.memory[a] = y = this.memory[( x & MASK ) >> 2] + this.accumulator + this.lastResult;
				this.results[a++] = this.lastResult = this.memory[( ( y >> SIZEL ) & MASK ) >> 2] + x;

				x = this.memory[a];
				this.accumulator ^= ( int ) ( ( uint ) this.accumulator >> 6 );
				this.accumulator += this.memory[b++];
				this.memory[a] = y = this.memory[( x & MASK ) >> 2] + this.accumulator + this.lastResult;
				this.results[a++] = this.lastResult = this.memory[( ( y >> SIZEL ) & MASK ) >> 2] + x;

				x = this.memory[a];
				this.accumulator ^= this.accumulator << 2;
				this.accumulator += this.memory[b++];
				this.memory[a] = y = this.memory[( x & MASK ) >> 2] + this.accumulator + this.lastResult;
				this.results[a++] = this.lastResult = this.memory[( ( y >> SIZEL ) & MASK ) >> 2] + x;

				x = this.memory[a];
				this.accumulator ^= ( int ) ( ( uint ) this.accumulator >> 16 );
				this.accumulator += this.memory[b++];
				this.memory[a] = y = this.memory[( x & MASK ) >> 2] + this.accumulator + this.lastResult;
				this.results[a++] = this.lastResult = this.memory[( ( y >> SIZEL ) & MASK ) >> 2] + x;
			}

			for ( b = 0; b < SIZE / 2; )
			{
				x = this.memory[a];
				this.accumulator ^= this.accumulator << 13;
				this.accumulator += this.memory[b++];
				this.memory[a] = y = this.memory[( x & MASK ) >> 2] + this.accumulator + this.lastResult;
				this.results[a++] = this.lastResult = this.memory[( ( y >> SIZEL ) & MASK ) >> 2] + x;

				x = this.memory[a];
				this.accumulator ^= ( int ) ( ( uint ) this.accumulator >> 6 );
				this.accumulator += this.memory[b++];
				this.memory[a] = y = this.memory[( x & MASK ) >> 2] + this.accumulator + this.lastResult;
				this.results[a++] = this.lastResult = this.memory[( ( y >> SIZEL ) & MASK ) >> 2] + x;

				x = this.memory[a];
				this.accumulator ^= this.accumulator << 2;
				this.accumulator += this.memory[b++];
				this.memory[a] = y = this.memory[( x & MASK ) >> 2] + this.accumulator + this.lastResult;
				this.results[a++] = this.lastResult = this.memory[( ( y >> SIZEL ) & MASK ) >> 2] + x;

				x = this.memory[a];
				this.accumulator ^= ( int ) ( ( uint ) this.accumulator >> 16 );
				this.accumulator += this.memory[b++];
				this.memory[a] = y = this.memory[( x & MASK ) >> 2] + this.accumulator + this.lastResult;
				this.results[a++] = this.lastResult = this.memory[( ( y >> SIZEL ) & MASK ) >> 2] + x;
			}
		}

		/**
		 * Get a random value.
		 * 
		 * @return A random value
		 */
		public int value( )
		{
			if ( this.count-- == 0 )
			{
				this.isaac();
				this.count = ( SIZE - 1 );
			}

			return this.results[this.count];
		}
	}
}
