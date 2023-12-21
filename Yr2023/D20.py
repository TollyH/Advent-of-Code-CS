# This Python script converts a Day 20 input file to a GraphViz DOT file.
# It is not required for the C# solution to work.
# A command such as the following can be used to render the file:
#     "dot -Tsvg <name_of_dot_file> -o <image_name>.svg"

# Colours:
# Black = Module with no output (e.g. "rx")
# Red = Module with outputs but no name prefix (e.g. "broadcaster")
# Green = Flip-flop module
# Blue = Conjunction module

import sys

if len(sys.argv) != 2:
    print(
        "This script takes a single command line argument "
        "- the path to the file to convert", file=sys.stderr
    )
    sys.exit(1)

with open(sys.argv[1], encoding="utf8") as input_file:
    input_lines = input_file.read().strip().splitlines()

connections: list[tuple[str, list[str]]] = []
no_prefix: list[str] = []
percent_prefix: list[str] = []
ampersand_prefix: list[str] = []

for line in input_lines:
    node, outputs = line.split(" -> ")
    if node.startswith('%'):
        name = node[1:]
        percent_prefix.append(name)
    elif node.startswith('&'):
        name = node[1:]
        ampersand_prefix.append(name)
    else:
        name = node
        no_prefix.append(name)
    connections.append((name, outputs.split(", ")))

print("digraph {")
print("    // Coloured nodes")
for node in no_prefix:
    print(f"    {node} [color=\"red\"];")
for node in percent_prefix:
    print(f"    {node} [color=\"green\"];")
for node in ampersand_prefix:
    print(f"    {node} [color=\"blue\"];")
print()
print("    // Connections")
for src, dst in connections:
    print(f"    {src} -> {{{' '.join(dst)}}};")
print("}")
