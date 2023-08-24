#!/bin/sh

find . -name "*.cs" | xargs clang-format -i
