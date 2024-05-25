using System.Collections.Generic;
using System;

namespace SnakeMLDesktop
{
    public class Settings
    {
        public Dictionary<string, object> Values { get; private set; }

        public Settings()
        {
            Values = new Dictionary<string, object>
        {
            { "board_size", new Tuple<int, int>(10, 10) },
            { "hidden_layer_activation", "relu" },
            { "output_layer_activation", "sigmoid" },
            { "hidden_network_architecture", new List<int> { 20, 12 } },
            { "vision_type", 8 },
            { "mutation_rate", 0.05 },
            { "mutation_rate_type", "static" },
            { "probability_gaussian", 1.0 },
            { "probability_random_uniform", 0.0 },
            { "SBX_eta", 100 },
            { "probability_SBX", 0.5 },
            { "SPBX_type", "r" },
            { "probability_SPBX", 0.5 },
            { "crossover_selection_type", "roulette_wheel" },
            { "num_parents", 500 },
            { "num_offspring", 1000 },
            { "selection_type", "plus" },
            { "lifespan", double.PositiveInfinity },
            { "apple_and_self_vision", "binary" }
        };
        }
    }

}
