package main

import (
	"plugin"
	"time"

	"common"
)

func TestLocalCall(ii common.I1) int64 {
	var a = time.Now().UnixNano()
	var v = 0
	for i := 0; i < 100000000; i++ {
		v += 1
	}
	var b = time.Now().UnixNano()
	return b - a
}

func TestPluginInnerCalll(ii common.I1) int64 {
	open, err := plugin.Open("/home/lin/gotest/p/p.so")
	if err != nil {
		panic(err)
	}
	lookup, err := open.Lookup("TestPluginInner")
	if err != nil {
		panic(err)
	}
	res := lookup.(func(ii common.I1) int64)
	var a = time.Now().UnixNano()
	res(ii)
	var b = time.Now().UnixNano()
	return b - a
}

func TestPluginOuterCalll(ii common.I1) int64 {
	open, err := plugin.Open("/home/lin/gotest/p/p.so")
	if err != nil {
		panic(err)
	}
	lookup, err := open.Lookup("TestPluginOuter")
	if err != nil {
		panic(err)
	}
	res := lookup.(func(common.I1) int)
	var a = time.Now().UnixNano()
	var v = 0
	for i := 0; i < 100000000; i++ {
		v += res(ii)
	}
	var b = time.Now().UnixNano()
	return b - a
}

func main() {
	println("local call", TestLocalCall(common.I1{}))
	println("-------------")
	println("plugin inner call", TestPluginInnerCalll(common.I1{}))
	println("-------------")
	println("plugin outer call", TestPluginOuterCalll(common.I1{}))
}
