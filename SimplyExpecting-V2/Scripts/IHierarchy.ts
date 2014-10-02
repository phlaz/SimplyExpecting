interface IHierarchy<TNode> {
    Parent: IHierarchy<TNode>;
    Children: Array<IHierarchy<TNode>>;
}

class MenuNode implements IHierarchy<MenuNode>{
    public Parent: MenuNode;
    public Children: Array<MenuNode> = [];
}