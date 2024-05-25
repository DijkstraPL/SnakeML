namespace SnakeML.NeuralNet
{
    internal class Matrix
    {
        public int Rows { get; }
        public int Columns { get; }

        public double[,] _matrix;

        public double this[int i, int j]
        {
            get => _matrix[i, j];
            set => _matrix[i, j] = value;
        }

        public Matrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            _matrix = new double[Rows, Columns];
        }
        public static Matrix SingleColumnMatrixFromArray(double[] arr)
        {
            Matrix matrix = new Matrix(arr.Length, 1);
            for (int i = 0; i < arr.Length; i++)
                matrix[i, 0] = arr[i];
            return matrix;
        }
        public void Multiply(double val)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                    _matrix[i, j] *= val;
            }
        }
        public double[] ToArray()
        {
            double[] arr = new double[Rows * Columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                    arr[j + i * Columns] = _matrix[i, j];
            }
            return arr;
        }
        public Matrix Dot(Matrix matrix)
        {
            var result = new Matrix(Rows, matrix.Columns);

            if (Columns != matrix.Rows)
                return result;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < Columns; k++)
                    {
                        sum += _matrix[i, k] * matrix[k, j];
                    }
                    result[i, j] = sum;
                }
            }

            return result;
        }

        public void Add(double val)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                    _matrix[i, j] += val;
            }
        }

        public Matrix Add(Matrix matrix)
        {
            Matrix newMatrix = new Matrix(Rows, Columns);
            if (Columns == matrix.Columns && Rows == matrix.Rows)
            {
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                        newMatrix[i, j] = _matrix[i, j] + matrix[i, j];
                }
            }
            return newMatrix;
        }

        public void Randomize()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                    _matrix[i, j] = GetRandomNumber(-1,1);
            }
        }
        public double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        public Matrix Subtract(Matrix matrix)
        {
            Matrix newMatrix = new Matrix(Columns, Rows);
            if (Columns == matrix.Columns && Rows == matrix.Rows)
            {
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                        newMatrix[i, j] = _matrix[i, j] - matrix[i, j];
                }
            }
            return newMatrix;
        }
        public Matrix Multiply(Matrix matrix)
        {
            Matrix newMatrix = new Matrix(Rows, Columns);
            if (Columns == matrix.Columns && Rows == matrix.Rows)
            {
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                        newMatrix[i, j] = _matrix[i, j] * matrix[i, j];
                }
            }
            return newMatrix;
        }
        public Matrix Transpose()
        {
            Matrix newMatrix = new Matrix(Columns, Rows);
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                    newMatrix[j, i] = _matrix[i, j];
            }
            return newMatrix;
        }
        public Matrix AddBias()
        {
            Matrix newMatrix = new Matrix(Rows + 1, 1);
            for (int i = 0; i < Rows; i++)
            {
                newMatrix[i, 0] = _matrix[i, 0];
            }
            newMatrix[Rows, 0] = 1;
            return newMatrix;
        }
        public Matrix Activate()
        {
            Matrix newMatrix = new Matrix(Rows, Columns);
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                    newMatrix[i, j] = Sigmoid(_matrix[i, j]);
            }
            return newMatrix;
        }

        private double Sigmoid(double val)
        {
            double y = 1.0 / (1 + Math.Pow(Math.E, -val));
            return y;
        }
        public Matrix SigmoidDerived()
        {
            Matrix newMatrix = new Matrix(Rows, Columns);
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                    newMatrix[i, j] = (_matrix[i, j] * (1 - _matrix[i, j]));
            }
            return newMatrix;
        }
        public Matrix RemoveBottomLayer()
        {
            Matrix newMatrix = new Matrix(Rows - 1, Columns);
            for (int i = 0; i < newMatrix.Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                    newMatrix[i, j] = _matrix[i, j];
            }
            return newMatrix;
        }

        public void Mutate(double mutationRate)
        {
            var random = new Random();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    double rand = random.NextDouble();
                    if (rand < mutationRate)
                    {//if chosen to be mutated
                        _matrix[i, j] += RandomGaussian() / 5;//add a random value to it(can be negative)

                        //set the boundaries to 1 and -1
                        if (_matrix[i, j] > 1)
                            _matrix[i, j] = 1;
                        if (_matrix[i, j] < -1)
                            _matrix[i, j] = -1;
                    }
                }
            }
        }

        private double RandomGaussian()
        {
            const double mean = 100;
            const double stdDev = 10;
            Random rand = new Random(); //reuse this if you are generating many
            double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal =
                         mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
            return randNormal;
        }

        public Matrix Crossover(Matrix partner)
        {
            Matrix child = new Matrix(Rows, Columns);
            var random = new Random();
            //pick a random point in the matrix
            int randC = random.Next(Columns);
            int randR = random.Next(Rows);
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {

                    if ((i < randR) || (i == randR && j <= randC))
                    { //if before the random point then copy from this matric
                        child[i, j] = _matrix[i, j];
                    }
                    else
                    { //if after the random point then copy from the parameter array
                        child[i, j] = partner[i, j];
                    }
                }
            }
            return child;
        }
        public Matrix Clone()
        {
            Matrix clone = new Matrix(Rows, Columns);
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                    clone[i, j] = _matrix[i, j];
            }
            return clone;
        }

        public void FromArray(double[] array)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    _matrix[i,j] = array[j + i * Columns];
                }
            }
        }
    }
}