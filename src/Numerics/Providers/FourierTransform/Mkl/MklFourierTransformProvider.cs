﻿// <copyright file="MklFourierTransformProvider.cs" company="Math.NET">
// Math.NET Numerics, part of the Math.NET Project
// http://mathnet.opensourcedotnet.info
//
// Copyright (c) 2009-2016 Math.NET
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

#if NATIVE

using System;
using System.Numerics;

namespace MathNet.Numerics.Providers.FourierTransform.Mkl
{
    public class MklFourierTransformProvider : IFourierTransformProvider
    {
        public void InitializeVerify()
        {
        }

        public void ForwardInplace(Complex[] complex, FourierTransformScaling scaling)
        {
            SafeNativeMethods.z_fft_forward_inplace(complex.Length, ForwardScaling(scaling, complex.Length), complex);
        }

        public void BackwardInplace(Complex[] complex, FourierTransformScaling scaling)
        {
            SafeNativeMethods.z_fft_backward_inplace(complex.Length, BackwardScaling(scaling, complex.Length), complex);
        }

        public Complex[] Forward(Complex[] complexTimeSpace, FourierTransformScaling scaling)
        {
            Complex[] work = new Complex[complexTimeSpace.Length];
            complexTimeSpace.Copy(work);
            ForwardInplace(work, scaling);
            return work;
        }

        public Complex[] Backward(Complex[] complexFrequenceSpace, FourierTransformScaling scaling)
        {
            Complex[] work = new Complex[complexFrequenceSpace.Length];
            complexFrequenceSpace.Copy(work);
            BackwardInplace(work, scaling);
            return work;
        }

        private double ForwardScaling(FourierTransformScaling scaling, int length)
        {
            switch (scaling)
            {
                case FourierTransformScaling.SymmetricScaling:
                    return Math.Sqrt(1.0/length);
                default:
                    return 1.0;
            }
        }

        private double BackwardScaling(FourierTransformScaling scaling, int length)
        {
            switch (scaling)
            {
                case FourierTransformScaling.SymmetricScaling:
                    return Math.Sqrt(1.0/length);
                case FourierTransformScaling.AsymmetricScaling:
                    return 1.0/length;
                default:
                    return 1.0;
            }
        }
    }
}

#endif
