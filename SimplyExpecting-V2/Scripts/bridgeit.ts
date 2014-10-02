

interface Bridgeit {
    camera(id: string, callback: (event: Event) => void, options: Object);
}

var bridgeit : Bridgeit = <Bridgeit>window["bridgeit"];