﻿using System;
using System.IO;
using System.Text;

using Ikadn.Utilities;

namespace Ikadn
{
	/// <summary>
	/// Base class for IKADN composers. Composers transform IKADN object to
	/// a plain text.
	/// </summary>
	public class IkadnWriter
	{
		/// <summary>
		/// Output stream where IKADN objects are being written.
		/// </summary>
		protected TextWriter Writer { get; private set; }

		/// <summary>
		/// Indentation level.
		/// </summary>
		public Indentation Indentation { get; protected set; }

		/// <summary>
		/// Temporary line contents.
		/// </summary>
		private StringBuilder Line { get; set; }

		/// <summary>
		/// Constructs basic IKADN composer.
		/// </summary>
		/// <param name="writer">Output stream.</param>
		public IkadnWriter(TextWriter writer)
		{
			this.Writer = writer;

			this.Line = new StringBuilder();
			this.Indentation = new Indentation();
		}
	
		public void Write(char character)
		{
			Write(character.ToString());
		}
		
		/// <summary>
		/// Appends a text to the current line. Text entered with this method
		/// is buffered and is not written immediately to the output stream.
		/// To finalize the buffered data (and write it to output stream) call
		/// either EndLine or WrtieLine.
		/// </summary>
		/// <param name="text">Raw text.</param>
		public void Write(string text)
		{
			if (text == null)
				throw new ArgumentNullException("text");

			Line.Append(text);
		}

		public void WriteLine(char character)
		{
			WriteLine(character.ToString());
		}
		
		/// <summary>
		/// Appends a text to the current line and writes buffered line to the
		/// output stream.
		/// </summary>
		/// <param name="text">Raw text.</param>
		public void WriteLine(string text)
		{
			if (text == null)
				throw new ArgumentNullException("text");

			Line.Append(text);
			EndLine();
		}

		/// <summary>
		/// Writes buffered line to the output stream.
		/// </summary>
		public void EndLine()
		{
			if (Line.Length > 0)
			{
				Writer.Write(Indentation);
				Writer.WriteLine(Line);
				Line.Length = 0;
			}
		}
	}
}
